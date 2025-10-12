using GolfImprovementMeasurement.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace GolfImprovementMeasurement.Services
{
    public class GolfDataDisplayer : IGolfDataDisplayer
    {
        private readonly TextWriter _output;

        public GolfDataDisplayer() : this(Console.Out)
        {
        }

        public GolfDataDisplayer(TextWriter output)
        {
            _output = output ?? throw new ArgumentNullException(nameof(output));
        }

        public void Display(string playerName, IEnumerable<GolfRound> rounds)
        {
            if (string.IsNullOrWhiteSpace(playerName))
            {
                throw new ArgumentException("Player name cannot be null or empty.", nameof(playerName));
            }

            ArgumentNullException.ThrowIfNull(rounds);

            _output.WriteLine($"{playerName} Data:");

            foreach (var round in rounds)
            {
                _output.WriteLine(
                    $"Days: {round.DaysSinceReference}, " +
                    $"Shots: {round.NumberOfShots}, " +
                    $"Condition: {round.CourseCondition}");
            }

            _output.WriteLine();
        }
    }
}
