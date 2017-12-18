using Original;
using System;
using System.Collections.Generic;
using Xunit;

namespace Tests
{
    public class OriginalTests
    {
        [Fact]
        public void NewContainerAllowsAddingFeatures()
        {
            var container1 = new Container(5);
            container1.AddFeature(ContainerFeature.VENTILATED);
            container1.AddFeature(ContainerFeature.ARMORED);
            Assert.Equal(2, container1.Features.Count);
        }

        [Fact]
        public void NewContainerAllowsAddingDrums()
        {
            var container1 = new Container(4);
            var sand = new Product();
            var drumSand = new Drum(sand, 4);
            container1.AddDrum(drumSand);
            Assert.Equal(1, container1.Drums.Count);
        }

        [Fact]
        public void NewContainerCanAccomodateDrumWithNoSpecification()
        {
            var container1 = new Container(4);
            var sand = new Product();
            var drumSand = new Drum(sand, 4);
            Assert.True(container1.CanAccommodate(drumSand));
        }

        [Fact]
        public void NewContainerCanAccomodateDrumWithValidSpecification()
        {
            var container1 = new Container(4);
            var specArmored = new ContainerSpecification(ContainerFeature.VENTILATED);

            var biologicalSamples = new Product();
            biologicalSamples.Specification = specArmored.Not();
            var drumBiologicalSamples = new Drum(biologicalSamples, 2);

            Assert.True(container1.CanAccommodate(drumBiologicalSamples));
        }

        [Fact]
        public void ContainerIsSafelyPacked()
        {
            var specArmored = new ContainerSpecification(ContainerFeature.ARMORED);
            var specVentilated = new ContainerSpecification(ContainerFeature.VENTILATED);

            var tnt = new Product();
            tnt.Specification = specArmored.And(specVentilated);
            var drumTNT = new Drum(tnt, 2);
            var sand = new Product();
            var drumSand = new Drum(sand, 4);
            var biologicalSamples = new Product();
            biologicalSamples.Specification = specArmored.Not();
            var drumBiologicalSamples = new Drum(biologicalSamples, 2);
            var ammonia = new Product();
            ammonia.Specification = specArmored.Not();
            var drumAmmonia = new Drum(ammonia, 2);
            var drums = new List<Drum>(4)
            {
                drumTNT,
                drumSand,
                drumBiologicalSamples,
                drumAmmonia
            };

            var containerCheap = new Container(6);
            var containerVentilated = new Container(6);
            containerVentilated.AddFeature(ContainerFeature.VENTILATED);
            var containerExpensive = new Container(6);
            containerExpensive.AddFeature(ContainerFeature.ARMORED);
            var containers = new List<Container>(3)
            {
                containerCheap,
                containerVentilated,
                containerExpensive
            };

            var packer = new Packer();
            packer.Pack(containers, drums);
            
            //var drum = new Drum(product1, 5);

            //var container1 = new Container(5);
            //container1.AddFeature(ContainerFeature.BLINDADO);
            //container1.AddDrum(drum);

            //Assert.True(container1.IsSafelyPacked());
        }
    }
}
