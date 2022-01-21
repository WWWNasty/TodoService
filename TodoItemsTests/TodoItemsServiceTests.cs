using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogicLayer.Implementation.Exceptions;
using BusinessLogicLayer.Implementation.Services;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Models.MapperProfiles;
using DataAccessLayer.Abstraction;
using DataAccessLayer.Abstraction.RepositoryInterfaces;
using DataAccessLayer.Models;
using DeepEqual.Syntax;
using Moq;
using Xunit;

namespace TodoItemsTests
{
    public class TodoItemsServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITodoItemRepository> _mockRepository;
        private readonly Mock<IUnitOfWork> _uowMock;


        public TodoItemsServiceTests()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _mockRepository = new Mock<ITodoItemRepository>();
            _uowMock.SetupGet(uowMock => uowMock.TodoItemRepository).Returns(_mockRepository.Object);

            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = config.CreateMapper();
        }

        [MemberData(nameof(GetDataForSuccessCreate))]
        [Theory]
        public async Task SuccessCreateItemTest(TodoItemDto itemToCreate)
        {
            var itemsService = new TodoItemsService(_uowMock.Object, _mapper);

            var createdItem = await itemsService.AddAsync(itemToCreate);
            
            itemToCreate.ShouldDeepEqual(createdItem);
            _mockRepository.Verify(mock => mock.Create(It.IsAny<TodoItem>()), Times.Once());
            _uowMock.Verify(mock => mock.SaveChangesAsync(), Times.Once());
        }

        [MemberData(nameof(GetDataForUnsuccessfulCreate))]
        [Theory]
        public async Task UnsuccessfulCreateItemTest(TodoItemDto itemToCreate)
        {
            var itemsService = new TodoItemsService(_uowMock.Object, _mapper);

            await Assert.ThrowsAsync<ValidationException>(async () => await itemsService.AddAsync(itemToCreate));
            _mockRepository.Verify(mock => mock.Create(It.IsAny<TodoItem>()), Times.Never());
            _uowMock.Verify(mock => mock.SaveChangesAsync(), Times.Never());
        }
        
        [MemberData(nameof(GetDataForSuccessUpdate))]
        [Theory]
        public async Task SuccessUpdateItemTest(TodoItemDto itemToUpdate)
        {
            _uowMock.Setup(a => a.TodoItemRepository.GetAsync(itemToUpdate.Id))
                .ReturnsAsync(new TodoItem
                {
                    Id = itemToUpdate.Id,
                    Name = itemToUpdate.Name,
                    IsComplete = itemToUpdate.IsComplete
                });
           
            var itemsService = new TodoItemsService(_uowMock.Object, _mapper);

            var createdItem = await itemsService.UpdateAsync(itemToUpdate);
            
            itemToUpdate.ShouldDeepEqual(createdItem);
            _mockRepository.Verify(mock => mock.Update(It.IsAny<TodoItem>()), Times.Once());
            _uowMock.Verify(mock => mock.SaveChangesAsync(), Times.Once());
        }
        
        [MemberData(nameof(GetDataForUnsuccessfulUpdate))]
        [Theory]
        public async Task UnsuccessfulUpdateItemTest(TodoItemDto itemToUpdate)
        { 
            _uowMock.Setup(a => a.TodoItemRepository.GetAsync(itemToUpdate.Id))
                .ReturnsAsync(new TodoItem()
                {
                    Id = itemToUpdate.Id,
                    Name = itemToUpdate.Name,
                    IsComplete = itemToUpdate.IsComplete
                });
            var itemsService = new TodoItemsService(_uowMock.Object, _mapper);

            await Assert.ThrowsAsync<ValidationException>(async () => await itemsService.UpdateAsync(itemToUpdate));
            _mockRepository.Verify(mock => mock.Update(It.IsAny<TodoItem>()), Times.Never());
            _uowMock.Verify(mock => mock.SaveChangesAsync(), Times.Never());
        }
        
        [MemberData(nameof(GetDataForUnsuccessfulUpdateNull))]
        [Theory]
        public async Task UnsuccessfulUpdateItemNullTest(TodoItemDto itemToUpdate)
        { 
            _uowMock.Setup(a => a.TodoItemRepository.GetAsync(itemToUpdate.Id))
                .ReturnsAsync( () => null);
            var itemsService = new TodoItemsService(_uowMock.Object, _mapper);

            await Assert.ThrowsAsync<TodoItemNotFoundException>(async () => await itemsService.UpdateAsync(itemToUpdate));
            _mockRepository.Verify(mock => mock.Update(It.IsAny<TodoItem>()), Times.Never());
            _uowMock.Verify(mock => mock.SaveChangesAsync(), Times.Never());
        }
        
        [Fact]
        public async Task SuccessDeleteItemTest()
        { 
            _uowMock.Setup(a => a.TodoItemRepository.GetAsync(1))
                .ReturnsAsync(new TodoItem
                {
                    Id = 1,
                    Name = "del test"
                });
            var itemsService = new TodoItemsService(_uowMock.Object, _mapper);

            await itemsService.DeleteAsync(1);
            
            _mockRepository.Verify(mock => mock.Delete(It.IsAny<TodoItem>()), Times.Once());
            _uowMock.Verify(mock => mock.SaveChangesAsync(), Times.Once());
        }
        
        [Fact]
        public async Task UnsuccessfulDeleteItemTest()
        { 
            _uowMock.Setup(a => a.TodoItemRepository.GetAsync(1))
                .ReturnsAsync(() => null);
            var itemsService = new TodoItemsService(_uowMock.Object, _mapper);

            await Assert.ThrowsAsync<TodoItemNotFoundException>(async () => await itemsService.DeleteAsync(1));
            _mockRepository.Verify(mock => mock.Delete(It.IsAny<TodoItem>()), Times.Never());
            _uowMock.Verify(mock => mock.SaveChangesAsync(), Times.Never());
        }
        
        [Fact]
        public async Task SuccessGetItemTest()
        { 
            _uowMock.Setup(a => a.TodoItemRepository.GetAsync(1))
                .ReturnsAsync(new TodoItem
                {
                    Id = 1,
                    Name = "del test"
                });
            var itemsService = new TodoItemsService(_uowMock.Object, _mapper);

            var getItem = await itemsService.GetAsync(1);
            
            Assert.Equal(1, getItem.Id);
            _mockRepository.Verify(mock => mock.GetAsync(1), Times.Once());
        }
        
        [Fact]
        public async Task UnsuccessfulGetItemTest()
        { 
            _uowMock.Setup(a => a.TodoItemRepository.GetAsync(1))
                .ReturnsAsync(() => null);
            var itemsService = new TodoItemsService(_uowMock.Object, _mapper);

            await Assert.ThrowsAsync<TodoItemNotFoundException>(async () => await itemsService.GetAsync(1));
            _mockRepository.Verify(mock => mock.GetAsync(new long()), Times.Never());
            _uowMock.Verify(mock => mock.SaveChangesAsync(), Times.Never());
        }
        
        [Fact]
        public async Task GetAllItemsTest()
        {
            var expected = GetAllItemsSetup();
            var itemsService = new TodoItemsService(_uowMock.Object, _mapper);

            var actual = await itemsService.GetAllAsync();
            
            actual.ShouldDeepEqual(expected);
            _mockRepository.Verify(mock => mock.GetAllAsync(), Times.Once());
        }

        private List<TodoItemDto> GetAllItemsSetup()
        {
            var testData = new List<TodoItem>
            {
                new TodoItem
                {
                    Id = 1,
                    Name = "test1",
                }
            };
            var expected = new List<TodoItemDto>
            {
                new TodoItemDto
                {
                    Id = testData[0].Id,
                    Name = testData[0].Name
                }
            };
            _uowMock.Setup(a => a.TodoItemRepository.GetAllAsync())
                .ReturnsAsync(testData);
            return expected;
        }

        public static TheoryData<TodoItemDto> GetDataForSuccessCreate() =>
             new TheoryData<TodoItemDto>
             {
                 new TodoItemDto
                 {
                     Name = "test",
                     IsComplete = true
                 }
             };

         public static TheoryData<TodoItemDto> GetDataForUnsuccessfulCreate() =>
             new TheoryData<TodoItemDto>
             {
                 TestData.WithoutNameForCreate(),
                 TestData.TooLongNameForCreate(),
             };

         public static TheoryData<TodoItemDto> GetDataForSuccessUpdate() =>
             new TheoryData<TodoItemDto>
             {
                 new TodoItemDto
                 {
                     Id = 1,
                     Name = "test",
                     IsComplete = true
                 }
             };
             
         public static TheoryData<TodoItemDto> GetDataForUnsuccessfulUpdate() =>
             new TheoryData<TodoItemDto>
             {
                 TestData.WithoutNameForUpdate(),
                 TestData.TooLongNameForUpdate(),
             };

         public static TheoryData<TodoItemDto> GetDataForUnsuccessfulUpdateNull() =>
             new TheoryData<TodoItemDto>
             {
                 new TodoItemDto
                 {
                     Id = 1,
                     Name = "test",
                     IsComplete = true
                 }
             };
    }
}