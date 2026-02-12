namespace SharedKernel.ValueObjects;

public record Quantity(int Value)
{
    public static Quantity From(int value) => value < 0 ? throw new ArgumentException() : new(value);
    public static Quantity Zero => new(0);
    public Quantity Min(Quantity other) => new(Math.Min(Value, other.Value));

    public static Quantity operator +(Quantity a, Quantity b) => new(a.Value + b.Value);
    public static Quantity operator -(Quantity a, Quantity b) => new(a.Value - b.Value);
}
