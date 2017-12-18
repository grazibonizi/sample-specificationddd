using System;
using System.Collections.Generic;

namespace Original
{
    public class Container
    {
        public IList<Drum> Drums { get; }
        public IList<ContainerFeature> Features { get; }
        public double Capacity { get; }

        public double RemainingSpace
        {
            get
            {
                double totalContentSize = 0;
                foreach (var drum in Drums)
                    totalContentSize += drum.Size;
                return Capacity - totalContentSize;
            }
        }

        public Container(int capacity)
        {
            Capacity = capacity;
            Features = new List<ContainerFeature>(0);
            Drums = new List<Drum>();
        }

        public void AddDrum(Drum drum)
        {
            Drums.Add(drum);
        }

        public void AddFeature(ContainerFeature feature)
        {
            Features.Add(feature);
        }

        public bool HasSpaceFor(Drum drum)
        {
            return RemainingSpace >= drum.Size;
        }

        public bool CanAccommodate(Drum drum)
        {
            if (drum.GetContainerSpecification() == null)
                return HasSpaceFor(drum);
            else
                return HasSpaceFor(drum) &&
                drum.GetContainerSpecification().IsSatisfiedBy(this);
        }
    }

    public class Drum
    {
        public Product Product { get; }
        public double Size { get; }

        public Drum(Product product, double size)
        {
            Product = product;
            Size = size;
        }

        public ISpecification<Container> GetContainerSpecification()
        {
            return Product.Specification;
        }
    }

    public class Product
    {
        public ISpecification<Container> Specification { get; set; }
    }

    public enum ContainerFeature
    {
        ARMORED,
        VENTILATED
    }

    public class Packer
    {
        public void Pack(IList<Container> containers, IList<Drum> drums)
        {
            foreach (var drum in drums)
            {
                var container = FindContainerFor(containers, drum);
                container.AddDrum(drum);
            }
        }

        public Container FindContainerFor(IList<Container> containers, Drum drum)
        {
            foreach (var container in containers)
                if (container.CanAccommodate(drum))
                    return container;
            throw new Exception();
        }
    }
}
