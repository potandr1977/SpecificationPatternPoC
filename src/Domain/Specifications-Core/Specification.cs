using System;
using System.Linq.Expressions;

namespace Domain.Specifications.Core;

/// <summary>
/// Базовый класс спецификации.
/// </summary>
/// <typeparam name="T">Тип модели данных, поля которой будут использованы при фильтрации.</typeparam>
public abstract class Specification<T>
{
    // Конвертируем нашу спецификацию в Expression.
    public abstract Expression<Func<T, bool>> ToExpression();

    // Проверяем наше условие на истинность.
    public virtual bool IsSatisfiedBy(T entity)
    {
        Func<T, bool> predicate = ToExpression().Compile();
        return predicate(entity);
    }

    /// <summary>
    /// Операция конъюнкции, логического "И" над двумя спецификациями.
    /// Над текущей и той, которая передана в качестве аргумента.
    /// из двух спецификаций генерируем новую.
    /// </summary>
    /// <param name="specification"></param>
    /// <returns></returns>
    public Specification<T> And(Specification<T> specification) =>
        new AndSpecification<T>(this, specification);

    /// <summary>
    /// Операция дизъюнкции, логического "ИЛИ" над двумя спецификациями. 
    /// Над текущей и той, которая передана в качестве аргумента.
    /// из двух спецификаций генерируем новую.
    /// </summary>
    /// <param name="specification"></param>
    /// <returns></returns>
    public Specification<T> Or(Specification<T> specification) =>
        new OrSpecification<T>(this, specification);

    /// <summary>
    /// Операция логического отрицания.
    /// Генерируем новую спецификацию, которая является отрицанием исходной.
    /// </summary>
    /// <returns></returns>
    public Specification<T> Not() => new NotSpecification<T>(this);

    /// <summary>
    /// Определяем false для спецификации.
    /// </summary>
    /// <param name="specification"></param>
    /// <returns></returns>
    public static bool operator false (Specification<T> specification) => false;

    /// <summary>
    /// Определяем true для спецификации.
    /// </summary>
    /// <param name="specification"></param>
    /// <returns></returns>
    public static bool operator true(Specification<T> specification) => true;

    /// <summary>
    /// Оператор логического "И".
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static Specification<T> operator &(Specification<T> left, Specification<T> right) =>
        left.And(right);

    /// <summary>
    /// Оператор логического "ИЛИ".
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static Specification<T> operator |(Specification<T> left, Specification<T> right) =>
        left.Or(right);

    /// <summary>
    /// Оператор логического отрицания.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static Specification<T> operator !(Specification<T> specification) => specification.Not();
}
