using System;
using Stryker.Shared.Tests;

namespace Stryker.Core.Initialisation;

public class TimeoutValueCalculator : ITimeoutValueCalculator
{
    private readonly int _extraMs;
    private readonly int _initializationTime;
    private readonly int _aggregatedTestTimes;
    private const double Ratio = 1.5;

    public TimeoutValueCalculator(int extraMs) => _extraMs = extraMs;

    public TimeoutValueCalculator(int extraMs, int testSessionTime, int aggregatedTestTimes)
    {
        _extraMs = extraMs;
        _initializationTime = Math.Max(testSessionTime - aggregatedTestTimes, 0);
        _aggregatedTestTimes = aggregatedTestTimes;
    }
    
    public int DefaultTimeout => CalculateTimeoutValue(_aggregatedTestTimes);

    public int CalculateTimeoutValue(int estimatedTime) => (int)((_initializationTime + estimatedTime) * Ratio) + _extraMs;
}
