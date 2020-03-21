using System;
using System.Linq;
using Xunit;

namespace ReservedSeatsChallenge
{
    public class Challenge
    {


        [Theory]
        [InlineData(0, "", 0)]
        [InlineData(1, "", 2)]
        [InlineData(3, "  ", 6)]
        [InlineData(2, "1A 2F 1C", 2)]
        [InlineData(1, "1E 1H", 0)]
        [InlineData(1, "1H", 1)]
        [InlineData(1, "1J", 1)]
        [InlineData(1, "1C", 1)]
        [InlineData(1, "1B", 1)]
        [InlineData(1, "1A", 2)]
        public void Test1(int rows, string seats, int expected)
        {
            Assert.Equal(expected, Solution(rows, seats));
        }

        public int Solution(int rows, string seats)
        {
            //ABC DEFG HJK
            const string leftSpot = "BCDE";
            const string rightSpot = "FGHJ";


            if (string.IsNullOrWhiteSpace(seats))
            {
                return rows * 2;
            }

            var freeSeats = seats.Split(" ")
                .OrderBy(x => x)
                .GroupBy(x => x.First())
                .Select(x =>
                {
                    var reservedSeatsInRow = x.Select(y => y.Last()).ToArray();
                    var leftSpotFree = reservedSeatsInRow.All(s => leftSpot.Contains(s) == false);
                    var rightSpotFree = reservedSeatsInRow.All(s => rightSpot.Contains(s) == false);

                    if (leftSpotFree && rightSpotFree)
                    {
                        return 2;
                    }

                    if (leftSpotFree)
                    {
                        return 1;
                    }

                    return rightSpotFree ? 1 : 0;
                }).Sum();

            return freeSeats;
        }
    }
}