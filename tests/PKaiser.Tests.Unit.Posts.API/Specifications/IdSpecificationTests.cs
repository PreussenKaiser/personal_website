using PKaiser.Posts.API.Domain.Specifications;
using PKaiser.Tests.Unit.Posts.API.Specifications.Entities;

namespace PKaiser.Tests.Unit.Posts.API.Specifications;

/// <summary>
/// 
/// </summary>
public sealed class IdSpecificationTests
{
	/// <summary>
	/// Asserts that the specification is satisified when the correct identifier is supplied.
	/// </summary>
	[Fact]
	public void CorrectIdSatisifed()
	{
		// Arrange
		MockEntity entity = new() { Id = Guid.NewGuid() };
		IdSpecification<MockEntity> specification = new(entity.Id);

		// Act
		bool result = specification.IsSatisifiedBy(entity);

		// Assert
		Assert.True(result);
	}

	/// <summary>
	/// Asserts that the specification is not satisfied when the incorrect identifier is supplied.
	/// </summary>
	[Fact]
	public void IncorrectIdNotSatisfied()
	{
		// Arrange
		MockEntity entity = new() { Id = Guid.NewGuid() };
		IdSpecification<MockEntity> specification = new(Guid.Empty);

		// Act
		bool result = specification.IsSatisifiedBy(entity);

		// Assert
		Assert.False(result);
	}
}
