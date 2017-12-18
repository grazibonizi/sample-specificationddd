using System;

namespace Original
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T candidate);
        ISpecification<T> And(ISpecification<T> other);
        ISpecification<T> Or(ISpecification<T> other);
        ISpecification<T> Not();
    }

    public abstract class AbstractSpecification<T> : ISpecification<T>
    {
        public virtual bool IsSatisfiedBy(T candidate)
        {
            throw new NotImplementedException();
        }

        public ISpecification<T> And(ISpecification<T> other)
        {
            return new AndSpecification<T>(this, other);
        }

        public ISpecification<T> Or(ISpecification<T> other)
        {
            return new OrSpecification<T>(this, other);
        }

        public ISpecification<T> Not()
        {
            return new NotSpecification<T>(this);
        }
    }

    public class AndSpecification<T> : AbstractSpecification<T>
    {
        ISpecification<T> one;
        ISpecification<T> other;

        public AndSpecification(ISpecification<T> x, ISpecification<T> y)
        {
            one = x;
            other = y;
        }

        public override bool IsSatisfiedBy(T candidate)
        {
            return one.IsSatisfiedBy(candidate) &&
                other.IsSatisfiedBy(candidate);
        }
    }

    public class OrSpecification<T> : AbstractSpecification<T>
    {
        ISpecification<T> one;
        ISpecification<T> other;

        public OrSpecification(ISpecification<T> x, ISpecification<T> y)
        {
            one = x;
            other = y;
        }

        public override bool IsSatisfiedBy(T candidate)
        {
            return one.IsSatisfiedBy(candidate) ||
                other.IsSatisfiedBy(candidate);
        }
    }

    public class NotSpecification<T> : AbstractSpecification<T>
    {
        ISpecification<T> wrapped;

        public NotSpecification(ISpecification<T> x)
        {
            wrapped = x;
        }

        public override bool IsSatisfiedBy(T candidate)
        {
            return !wrapped.IsSatisfiedBy(candidate);
        }
    }
}
