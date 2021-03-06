﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TestFlightAPI;

namespace TestFlight
{
    public class TestFlightReliability_SkinTemperature : TestFlightReliabilityBase
    {
        [KSPField]
        public FloatCurve temperatureCurve = new FloatCurve();

        private double oldPenalty = TestFlightUtil.MIN_FAILURE_RATE;

        public override void OnUpdate()
        {
            if (!TestFlightEnabled)
                return;

            if (core == null)
                return;

            double newPenalty = temperatureCurve.Evaluate((float)base.part.skinTemperature);
            newPenalty = Math.Min(newPenalty, TestFlightUtil.MIN_FAILURE_RATE);

            if (newPenalty != oldPenalty)
                core.SetTriggerMomentaryFailureModifier("SkinTemperature", newPenalty, this);
            oldPenalty = newPenalty;
        }

        public override double GetBaseFailureRate(float flightData)
        {
            return 0;
        }
    }
}
