namespace LearningSystem.Common.Constants
{
	public static class DataConstants
    {
		public class Student
		{
			public const int UsernameMinLength = 1;

			public const int UsernameMaxLength = 20;

			public const int NameMinLength = 1;

			public const int NameMaxLength = 100;

			public const int PasswordMinLength = 3;

            public const int PasswordMaxLength = 100;
		}

		public class Course
		{
			public const int NameMinLength = 1;

			public const int NameMaxLength = 100;
		}

		public class Article
		{
			public const int TitleMinLength = 1;

			public const int TitleMaxLength = 255;
		}

		public class Error
		{
			public const string NameLength = "{0} must be between {2} and {1} symbols long inclusive!";

			public const string ArticleTitleLength = "{0} must be between {2} and {1} symbols long inclusive!";

            public const string PasswordLength = "The {0} must be at least {2} and at max {1} characters long.";

            public const string PasswordsMismatch = "The password and confirmation password do not match.";
        }
		
		public class Solution
		{
			public const int SolutionMaxFileSize = 2000000;
		} 
	}
}
