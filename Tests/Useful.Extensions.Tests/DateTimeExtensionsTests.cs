﻿using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Useful.Extensions.Tests
{
    /// <summary>
    /// Date Time Extension Tests.
    /// </summary>
    public class DateTimeExtensionsTests
    {
        #region ShouldBeWithinRangeOf

        public static IEnumerable<object[]> ValidDates
        {
            get
            {
                return new[]
                {
                   new object[] { new DateTime(2017, 01, 01, 12, 30, 00), new DateTime(2017, 01, 01, 12, 30, 02) },
                   new object[] { new DateTime(2017, 01, 01, 12, 30, 00), new DateTime(2017, 01, 01, 12, 30, 09) },
                   new object[] { new DateTime(2017, 01, 01, 12, 30, 00), new DateTime(2017, 01, 01, 12, 29, 51) }
                };
            }
        }

        public static IEnumerable<object[]> InValidDates
        {
            get
            {
                return new[]
                {
                   new object[] { new DateTime(2017, 01, 01, 12, 30, 00), new DateTime(2017, 01, 01, 12, 30, 30) },
                   new object[] { new DateTime(2017, 01, 01, 12, 30, 00), new DateTime(2017, 01, 01, 12, 30, 10) },
                   new object[] { new DateTime(2017, 01, 01, 12, 30, 00), new DateTime(2017, 01, 01, 12, 29, 50) }
                };
            }
        }

        [Theory, MemberData(nameof(ValidDates))]
        public void test_should_be_within_range_with_valid_dates_in_range_returns_true_using_default(
            DateTime dateToCheck,
            DateTime expectedDateToCheck)
        {
            // Arrange.
            // Act.
            var result = dateToCheck.ShouldBeWithinRangeOf(expectedDateToCheck);

            // Assert.
            result.Should().BeTrue();
        }

        [Theory, MemberData(nameof(InValidDates))]
        public void test_should_be_within_range_with_invalid_dates_in_range_returns_false_using_default(
            DateTime dateToCheck,
            DateTime expectedDateToCheck)
        {
            // Arrange.
            // Act.
            var result = dateToCheck.ShouldBeWithinRangeOf(expectedDateToCheck);

            // Assert.
            result.Should().BeFalse();
        }

        [Fact]
        public void test_should_be_within_range_with_valid_date_returns_true_using_range()
        {
            // Arrange.
            var dateToCheck = new DateTime(2017, 01, 01, 12, 30, 00);
            var expectedDateToCheck = new DateTime(2017, 01, 01, 12, 30, 15);

            // Act.
            var result = dateToCheck.ShouldBeWithinRangeOf(expectedDateToCheck, 20);

            // Assert.
            result.Should().BeTrue();
        }

        [Fact]
        public void test_should_be_within_range_with_valid_date_returns_false_using_range()
        {
            // Arrange.
            var dateToCheck = new DateTime(2017, 01, 01, 12, 30, 00);
            var expectedDateToCheck = new DateTime(2017, 01, 01, 12, 30, 25);

            // Act.
            var result = dateToCheck.ShouldBeWithinRangeOf(expectedDateToCheck, 20);

            // Assert.
            result.Should().BeFalse();
        }

        #endregion ShouldBeWithinRangeOf

        #region Between

        public static IEnumerable<object[]> DateRangeTests
        {
            get
            {
                return new[]
                {
                   // Current.
                   new object[] { DateTime.Today, DateTime.Today.AddDays(-1), DateTime.Today.AddDays(1), true, true },
                   new object[] { DateTime.Today, DateTime.Today.AddDays(-1), DateTime.Today.AddDays(1), false, true },

                   // Past.
                   new object[] { DateTime.Today, DateTime.Today.AddDays(-2), DateTime.Today.AddDays(-1), true, false },
                   new object[] { DateTime.Today, DateTime.Today.AddDays(-2), DateTime.Today.AddDays(-1), false, false },

                   // Future.
                   new object[] { DateTime.Today, DateTime.Today.AddDays(1), DateTime.Today.AddDays(2), true, false },
                   new object[] { DateTime.Today, DateTime.Today.AddDays(1), DateTime.Today.AddDays(2), false, false },

                   // Same start date.
                   new object[] { DateTime.Today, DateTime.Today, DateTime.Today.AddDays(1), true, true },
                   new object[] { DateTime.Today, DateTime.Today, DateTime.Today.AddDays(1), false, false },

                   // Same end date.
                   new object[] { DateTime.Today, DateTime.Today.AddDays(-1), DateTime.Today, true, true },
                   new object[] { DateTime.Today, DateTime.Today.AddDays(-1), DateTime.Today, false, false },
                };
            }
        }

        [Theory, MemberData(nameof(DateRangeTests))]
        public void test_between_returns_expected_values(DateTime dateToCheck, DateTime startOfRange, DateTime endOfRange, bool inclusive, bool expectedResult)
        {
            // Arrange.
            // Act.
            var result = dateToCheck.Between(startOfRange, endOfRange, inclusive);

            // Assert.
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void test_between_inclusive_parameter_defaults_to_false()
        {
            // Arrange.
            // Act.
            var result = DateTime.Today.Between(DateTime.Today, DateTime.Today.AddDays(1));

            // Assert.
            result.Should().Be(false);
        }

        #endregion Between
    }
}