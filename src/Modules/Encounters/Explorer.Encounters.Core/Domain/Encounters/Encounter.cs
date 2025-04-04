﻿using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain.Encounters
{
    public enum EncounterStatus
    {
        Draft,
        Active,
        Archived
    }
    public enum EncounterType
    {
        Social,
        Location,
        Misc
    }
    public enum EncounterCreator
    {
        AuthorRequired,
        Author,
        Tourist
    }
    public class Encounter : Entity
    {
        public int UserId { get; private set; }
        public int KeyPointId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Xp { get; private set; }
        public Coordinates Coordinates { get; private set; }
        public EncounterStatus Status { get; private set; }
        public EncounterType Type { get; private set; }
        public EncounterCreator Creator { get; private set; }

        public int? Range { get; private set; }
        public int? TouristNumber { get; private set; }
        public int? ImagePath { get; private set; }

        public Encounter() { }
        public Encounter(int userId, int keyPointId, string name, string description, int xp, Coordinates coordinates, EncounterStatus status, EncounterType type, EncounterCreator creator)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");

            UserId = userId;
            KeyPointId = keyPointId;
            Name = name;
            Description = description;
            Xp = xp;
            Coordinates = coordinates;
            Status = status;
            Type = type;
            Creator = creator;
        }

        public Encounter(int userId, int keyPointId, string name, string description, int xp, Coordinates coordinates, EncounterStatus status, EncounterType type, EncounterCreator creator, int range, int touristNumber)
        {
            UserId = userId;
            KeyPointId = keyPointId;
            Name = name;
            Description = description;
            Xp = xp;
            Coordinates = coordinates;
            Status = status;
            Type = type;
            Creator = creator;
            Range = range;
            TouristNumber = touristNumber;
        }

        public void setStatus(EncounterStatus status)
        {
            this.Status = status;
        }
        public Encounter(int userId, int keyPointId, string name, string description, int xp, Coordinates coordinates, EncounterStatus status, EncounterType type, EncounterCreator creator, int range,int touristNumber, int imagePath)
        {
            UserId = userId;
            KeyPointId = keyPointId;
            Name = name;
            Description = description;
            Xp = xp;
            Coordinates = coordinates;
            Status = status;
            Type = type;
            Creator = creator;
            Range = range;
            TouristNumber = touristNumber;
            ImagePath = imagePath;
        }
    }

}
