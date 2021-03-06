﻿using NUnit.Framework;
using swapi_based_resupply_distance.Enums;
using swapi_based_resupply_distance.Interfaces;
using Tests;

namespace Swapi_based_resupply_distance.tests
{
	public class TestParser : TestContainers
	{
		private IParser parser;

		[SetUp]
		public void Setup()
		{
			parser = container.GetInstance<IParser>();
		}

		[Test]
		public void Test_time_unit_day()
		{
			var unit = parser.ParseTimeUnit("day");
			var expected = TimeUnit.Day;

			Assert.AreEqual(expected, unit);
		}

		[Test]
		public void Test_time_unit_days()
		{
			var unit = parser.ParseTimeUnit("days");
			var expected = TimeUnit.Day;

			Assert.AreEqual(expected, unit);
		}

		[Test]
		public void Test_time_unit_week()
		{
			var unit = parser.ParseTimeUnit("week");
			var expected = TimeUnit.Week;

			Assert.AreEqual(expected, unit);
		}

		[Test]
		public void Test_time_unit_weeks()
		{
			var unit = parser.ParseTimeUnit("weeks");
			var expected = TimeUnit.Week;

			Assert.AreEqual(expected, unit);
		}

		[Test]
		public void Test_time_unit_month()
		{
			var unit = parser.ParseTimeUnit("month");
			var expected = TimeUnit.Month;

			Assert.AreEqual(expected, unit);
		}

		[Test]
		public void Test_time_unit_months()
		{
			var unit = parser.ParseTimeUnit("months");
			var expected = TimeUnit.Month;

			Assert.AreEqual(expected, unit);
		}

		[Test]
		public void Test_time_unit_year()
		{
			var unit = parser.ParseTimeUnit("year");
			var expected = TimeUnit.Year;

			Assert.AreEqual(expected, unit);
		}

		[Test]
		public void Test_time_unit_years()
		{
			var unit = parser.ParseTimeUnit("years");
			var expected = TimeUnit.Year;

			Assert.AreEqual(expected, unit);
		}

		[Test]
		public void Test_time_unit_invalid()
		{
			var unit = parser.ParseTimeUnit("");
			var expected = TimeUnit.Unknown;

			Assert.AreEqual(expected, unit);
		}

		[Test]
		public void Test_int()
		{
			var number = parser.ParseInt("89");
			var expected = 89;

			Assert.AreEqual(expected, number);
		}

		[Test]
		public void Test_int_invalid()
		{
			var number = parser.ParseInt("unknown");
			var expected = -1;

			Assert.AreEqual(expected, number);
		}

		[Test]
		public void Test_duration()
		{
			var duration = parser.ParseDuration("3 years");

			Assert.AreEqual(TimeUnit.Year, duration.Unit);
			Assert.AreEqual(3, duration.Time);
		}

		[Test]
		public void Test_duration_switched()
		{
			var duration = parser.ParseDuration("years 3");

			Assert.AreEqual(TimeUnit.Unknown, duration.Unit);
			Assert.AreEqual(-1, duration.Time);
		}

		[Test]
		public void Test_duration_null()
		{
			parser.ParseDuration(null);
		}

		[Test]
		public void Test_duration_invalid()
		{
			var duration = parser.ParseDuration("3years");

			Assert.AreEqual(TimeUnit.Unknown, duration.Unit);
			Assert.AreEqual(-1, duration.Time);
		}
	}
}
