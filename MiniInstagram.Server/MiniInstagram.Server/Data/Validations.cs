namespace MiniInstagram.Server.Data
{
    public class Validations
    {
        public class Game
        {
            public const int MAX_DESCRIPTION_LENGTH = 3000;
            public const int MAX_TITLE_LENGTH = 30;
        }

        public class User
        {
            public const int MAX_BIOGRAPHY_LENGTH = 300;
        }
    }
}
