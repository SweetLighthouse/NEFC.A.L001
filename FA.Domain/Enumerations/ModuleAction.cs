namespace FA.Domain.Enumerations;

public class ModuleAction
{
    public static class Post
    {
        public static readonly ModuleAction Create = new(nameof(Create));
        public static readonly ModuleAction Update = new(nameof(Update));
        public static readonly ModuleAction Details = new(nameof(Details));
        public static readonly ModuleAction Index = new(nameof(Index));
        public static readonly ModuleAction Delete = new(nameof(Delete));
        public static readonly ModuleAction Publish = new(nameof(Publish));
        public static readonly ModuleAction Unpublish = new(nameof(Unpublish));
    }

    public static class Blog
    {
        public static readonly ModuleAction Create = new(nameof(Create));
        public static readonly ModuleAction Update = new(nameof(Update));
        public static readonly ModuleAction Details = new(nameof(Details));
        public static readonly ModuleAction Index = new(nameof(Index));
        public static readonly ModuleAction Delete = new(nameof(Delete));
    }

    public static class Category
    {
        public static readonly ModuleAction Create = new(nameof(Create));
        public static readonly ModuleAction Update = new(nameof(Update));
        public static readonly ModuleAction Details = new(nameof(Details));
        public static readonly ModuleAction Index = new(nameof(Index));
        public static readonly ModuleAction Delete = new(nameof(Delete));
    }

    public static class Tag
    {
        public static readonly ModuleAction Create = new(nameof(Create));
        public static readonly ModuleAction Update = new(nameof(Update));
        public static readonly ModuleAction Details = new(nameof(Details));
        public static readonly ModuleAction Index = new(nameof(Index));
        public static readonly ModuleAction Delete = new(nameof(Delete));
    }

    public static class Comment
    {
        public static readonly ModuleAction Create = new(nameof(Create));
        public static readonly ModuleAction Update = new(nameof(Update));
        public static readonly ModuleAction Details = new(nameof(Details));
        public static readonly ModuleAction Index = new(nameof(Index));
        public static readonly ModuleAction Delete = new(nameof(Delete));
    }

    public static class Role
    {
        public static readonly ModuleAction Create = new(nameof(Create));
        public static readonly ModuleAction Update = new(nameof(Update));
        public static readonly ModuleAction Details = new(nameof(Details));
        public static readonly ModuleAction Index = new(nameof(Index));
        public static readonly ModuleAction Delete = new(nameof(Delete));
    }

    public static class User
    {
        public static readonly ModuleAction Create = new(nameof(Create));
        public static readonly ModuleAction Update = new(nameof(Update));
        public static readonly ModuleAction Details = new(nameof(Details));
        public static readonly ModuleAction Index = new(nameof(Index));
        public static readonly ModuleAction Delete = new(nameof(Delete));
    }


    public string Name { get; }

    private ModuleAction(string name) => Name = name;

    public override string ToString() => Name;
}
