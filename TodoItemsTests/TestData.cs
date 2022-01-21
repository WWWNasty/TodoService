using BusinessLogicLayer.Models;

namespace TodoItemsTests
{
    public static class TestData
    {
        public static TodoItemDto WithoutNameForCreate() => 
            new TodoItemDto();

        public static TodoItemDto TooLongNameForCreate() =>
            new TodoItemDto
            {
                Name = "more_than_101_charsfsdfsdgsfgdsfgdfgdfgdfgergwrtergergergewrgregwergfsdsfgdfgdfgdfgergwrtergergergewrgregwerg",
            };

        public static TodoItemDto WithoutNameForUpdate() =>
            new TodoItemDto
            {
                Id = 1,
            };

        public static TodoItemDto TooLongNameForUpdate() =>
            new TodoItemDto
            {
                Id = 2,
                Name =
                    "more than 101 12hjdcidjfciujdsfoivujdsfiuvjdfiuvjdfiuvjiufjvdfvuifnvnfvhndfhjvnrunuhrenvhurnvtuihrntvu",
            };
    }
}