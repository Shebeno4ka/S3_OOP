namespace Itmo.ObjectOrientedProgramming.Lab1;

/// <summary>
/// Interface for any force applicable object, ex.: Train
/// </summary>
public interface IForceApplicable
{
    /// <summary>
    /// Gets получает текущую скорость объекта.
    /// </summary>
    double Speed { get; }

    /// <summary>
    /// Gets получает точность объекта.
    /// </summary>
    double Accuracy { get; }

    /// <summary>
    /// Применяет силу к объекту.
    /// </summary>
    /// <param name="force">Сила, которую нужно применить.</param>
    /// <returns>Возвращает true, если сила была успешно применена; в противном случае false.</returns>
    bool TryApplyForce(double force);

    /// <summary>
    /// Останавливает объект.
    /// </summary>
    void Halt();
}