public interface IActionModel
{
}

public static class IActionModelExtantion
{
    public static int Id(this IActionModel model) => 0;

    public static int Cost(this IActionModel model) => 0;

    public static bool IsCreateAction(this IActionModel model) => false;
}

