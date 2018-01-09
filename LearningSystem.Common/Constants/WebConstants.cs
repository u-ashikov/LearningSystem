namespace LearningSystem.Common.Constants
{
	public static class WebConstants
    {
		public const int ArticleContentMinLength = 255;

		public const int UsersPageSize = 10;

		public const int CoursesPageSize = 10;

		public const int ArticlesPageSize = 6;

		public const int TrainerCoursesPageSize = 5;

		public const int StudentCoursesPageSize = 5;

        public const int MinPageSize = 1;

		public const string BlogAuthorRole = "BlogAuthor";

        public const string TrainerRole = "Trainer";

        public const string FileAvailableExtension = ".zip";

		public const string SolutionContentType = "application/zip";

		public const string CertificateContentType = "application/pdf";

		public const string SolutionDownloadName = "exam-solution-{0}.zip";

		public const string CertificateDownloadName = "Certificate-{0}.pdf";

		public const string SuccessAddedUserToRole = "Successfully added roles to user {0} !";

		public const string NonExistingUser = "There is no such user with id: {0} !";

		public const string NonExistingCourse = "There is no such course with id: {0} !";

		public const string CourseStartDateInFuture = "The course start date must be in the future!";

		public const string CourseEndDateAfterStartDate = "The course end date must be after start date!";

		public const string SuccessCourseCreation = "Successfully created course {0} with trainer {1} !";

		public const string SuccessCourseEdit = "Successfully edited course with id:{0} !";

		public const string AlreadyInCourse = "You are already in that course !";

        public const string CourseStarted = "Course has already started!";

        public const string CourseInProgress = "Course has not finished yet!";

        public const string NotCourseLastDay = "You can upload solution on the last day of the course!";

		public const string NotInCourse = "Student with id: {0} is not in that course !";

		public const string SuccessSignUpForCourse = "You successfully signed up for course: {0} !";

		public const string SuccessSignOutFromCourse = "You successfully signed out from course: {0} !";

		public const string SuccessArticleCreation = "Successfully created article !";

		public const string NonExistingArticle = "There is no such article with id: {0} !";

		public const string NotProfileOwner = "You are not owner of that profile!";

		public const string FileSizeError = "Uploaded file size cannot be larger than 2MB!";

		public const string FileAcceptedFormatError = "Uploaded file has to be zip!";

		public const string SuccessUploadSolutionForCourse = "Successfully uploaded solution for course {0}!";

		public const string NonExistingTrainer = "There is no such trainer with id: {0} !";

		public const string NotCourseTrainer = "You are not trainer of that course!";

		public const string NonExistingStudent = "There is no such student with id: {0} !";

        public const string EmptyUsername = "Username cannot be empty!";

        public const string EmptyName = "Name cannot be empty!";

        public const string IncorrectOldPassword = "Old password is incorrect.";

        public class Area
        {
            public const string Admin = "Admin";

            public const string Blog = "Blog";
        }

        public class Routing
        {
            public const string AdminBaseRoute = "admin";
            public const string BlogBaseRoute = "blog";
            public const string CoursesBaseRoute = "courses";
            public const string StudentsBaseRoute = "students";

            public const string AdminAddCourse = "courses/add";
            public const string AdminEditCourse = "courses/edit/{id}";

            public const string AdminAllUsers = "users/all";
            public const string AdminAddUserToRole = "users/addtorole/{id}";

            public const string AddArticle = "articles/add";
            public const string AllArticles = "AllArticles";

            public const string BlogArticles = "blog/articles";
            public const string ArticleDetails = "details/{id}";

            public const string AllCourses = "all";
            public const string CourseDetails = "details/{id}";

            public const string SignUpForCourse = "signupforcourse/{courseId}";
            public const string SignOutFromCourse = "signoutfromcourse/{courseId}";
            public const string MyCourses = "mycourses/{id}";
            public const string UploadSolution = "mycourses/{courseId}/upload";
            public const string CourseCertificate = "courses/certificate/{courseId}";

            public const string TrainerCourses = "trainer/courses/{id}";
            public const string DownloadSolutionById = "{id}";

            public const string ById = "{id}";
        }

        public class Display
        {
            public const string Trainer = "Trainer";

            public const string StartDate = "Start date";

            public const string EndDate = "End date";

            public const string CurrentUserRoler = "Current Roles";

            public const string Email = "Email";

            public const string Password = "Password";

            public const string ConfirmPassword = "Confirm password";

            public const string OldPassword = "Old Password";

            public const string NewPassword = "New password";

            public const string RememberMe = "Remember me?";

            public const string PhoneNumber = "Phone number";
        }

        public class TempDataKey
        {
            public const string Message = "message";

            public const string AlertType = "type";
        }

        public class Controller
        {
            public const string Courses = "Courses";

            public const string Home = "Home";

            public const string Account = "Account";
        }
    }
}
