using System.Collections.Generic;

public interface IInputProfile
{
    string Name { get; }
    IReadOnlyDictionary<int, string> Axis { get; }
    IReadOnlyDictionary<int, string> Buttons { get; }
}
