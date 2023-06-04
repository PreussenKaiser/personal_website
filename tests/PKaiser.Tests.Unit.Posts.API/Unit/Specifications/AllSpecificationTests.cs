using PKaiser.Posts.API.Domain.Specifications;
using PKaiser.Tests.Unit.Posts.API.Unit.Specifications.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKaiser.Tests.Unit.Posts.API.Unit.Specifications;

/// <summary>
/// Tests for <see cref="AllSpecification{TEntity}"/>.
/// </summary>
public sealed class AllSpecificationTests
{
	/// <summary>
	/// Asserts that the specification is satisfied.
	/// </summary>
	[Fact]
	public void IsSatisified()
	{
		// Arrange
		MockEntity entity = new();
		AllSpecification<MockEntity> specification = new();

		// Act
		bool result = specification.IsSatisifiedBy(entity);

		// Assert
		Assert.True(result);
	}
}
