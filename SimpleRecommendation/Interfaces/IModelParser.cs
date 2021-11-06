namespace SimpleRecommendation
{
    public interface IModelParser
    {
        T GenerateModel<T>(string lineOfData) where T : class, new();
    }
}