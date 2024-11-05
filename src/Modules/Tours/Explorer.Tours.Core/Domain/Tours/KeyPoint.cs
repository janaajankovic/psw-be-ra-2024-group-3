﻿using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain.Tours
{
    public class KeyPoint : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string ImagePath { get; private set; }
        public long TourId { get; private set; }
        public Coordinates Coordinates { get; private set; }
        private KeyPoint() { }
        public KeyPoint(string name, string description, string imagePath, long tourId, Coordinates coordinates)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Invalid Description.");
            if (string.IsNullOrWhiteSpace(imagePath)) throw new ArgumentException("Invalid Image Path.");
            if (tourId < 0) throw new ArgumentException("Invalid Author Id.");
           

            Name = name;
            Description = description;
            ImagePath = imagePath;
            TourId = tourId;
            Coordinates = coordinates;

        }

        public double GetDistance(Coordinates desiredCoordinates)
        {
            return Math.Sqrt(Math.Pow(desiredCoordinates.Latitude - Coordinates.Latitude, 2) + Math.Pow(desiredCoordinates.Longitude - Coordinates.Longitude, 2));
        }

        public bool IsInDesiredDistance(Coordinates desiredCoordinates, double distance)
        {
            var actualDistance = GetDistance(desiredCoordinates);
            return actualDistance < distance;
        }

    }
}
