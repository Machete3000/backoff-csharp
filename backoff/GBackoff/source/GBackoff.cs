﻿using System;
namespace gbase {
    public class Backoff { 
        private static double DEFAULT_MIN_TIME_ = 1;
        private static double DEFAULT_MAX_TIME_ = 10;

        private static int DEFAULT_FACTOR_TIME_ = 2;

        private double minTime_; 
        private double maxTime_;

        private int factorTime_;

        private bool isJitter_;
        private int attempt_;

        private Random random_;


        /// <summary>
        /// Initializes a new instance of the <see cref="T:gbase.Backoff"/> class.
        /// </summary>
        /// <param name="minTime">Minimum time.</param>
        /// <param name="maxTime">Max time.</param>
        /// <param name="factorTime">Factor time.</param>
        /// <param name="isJitter">Is jitter.</param>
        public Backoff() {
            attempt_ = 0; 

            factorTime_ = DEFAULT_FACTOR_TIME_;
           
            minTime_ = DEFAULT_MIN_TIME_;
            maxTime_ = DEFAULT_MAX_TIME_;
            isJitter_ = false;

            random_ = new Random();
        }

        public void SetMinTime(double minTime) {
            minTime_ = minTime;
        }

        public double getMinTime() {
            return minTime_;
        }

        public void SetMaxTime(double maxTime) {
            maxTime_ = maxTime;
        }

        public double GetMaxTime() {
            return maxTime_;
        }

        public void SetFactorTime(int factorTime) {
            factorTime_ = factorTime;
        }

        public int GetFactorTime() {
            return factorTime_;
        }

        public void EnableJitter(bool isJitter) {
            isJitter_ = isJitter;
        }

        public double GetDuration() {
            double duration = CalculateTime();
            attempt_++;
            return duration;
        }

        public int GetAttempt() {
            return attempt_;
        }

        public void Reset() {
            attempt_ = 0;
        }

        private double CalculateTime() {
            double duration = minTime_ * Math.Pow(factorTime_, attempt_);

            if (isJitter_) {
                duration = (random_.NextDouble() * (duration - minTime_)) + minTime_;
            }

            if (duration > maxTime_) {
                duration = maxTime_;
            }

            return duration;
        }
    }
}