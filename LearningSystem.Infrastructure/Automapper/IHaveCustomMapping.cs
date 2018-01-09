namespace LearningSystem.Infrastructure.Automapper
{
	using AutoMapper;

	public interface IHaveCustomMapping
    {
		void ConfigureMapping(Profile mapper);
	}
}
