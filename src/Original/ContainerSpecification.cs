namespace Original
{
    public class ContainerSpecification : AbstractSpecification<Container>
    {
        public ContainerFeature requiredFeature { get; set; }
        public ContainerSpecification(ContainerFeature required)
        {
            requiredFeature = required;
        }

        public override bool IsSatisfiedBy(Container candidate)
        {
            return candidate.Features.Contains(requiredFeature);
        }
    }
}
