using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

class DataRepository
{
    private List<Room> rooms;
    private List<Subject> subjects;
    private FileManager fileManager;

    public DataRepository()
    {
        fileManager = new FileManager();
        rooms = fileManager.LoadFromFile<Room>("rooms.json");
        subjects = fileManager.LoadFromFile<Subject>("subjects.json");
    }

    public void SaveData()
    {
        SaveToFile("rooms.json", rooms);
        SaveToFile("subjects.json", subjects);
    }

    public void AddRoom(Room room)
    {
        rooms.Add(room);
    }

    public void AddSubject(Subject subject)
    {
        subjects.Add(subject);
    }

    public List<Room> GetAllRooms()
    {
        return rooms;
    }

    public List<Subject> GetAllSubjects()
    {
        return subjects;
    }

    public void UpdateRoomReservation(int roomNumber, Subject subject)
    {
        var existingRoom = rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);

        if (existingRoom != null)
        {
            var reservation = new Reservation
            {
                ReservedBy = subject.ReservedBy,
                StartTime = subject.StartTime,
                EndTime = subject.EndTime,
                SubjectName = subject.SubjectName
            };

            existingRoom.Reservations.Add(reservation);
            existingRoom.ReservedSubject = reservation;
            existingRoom.IsReserved = true;

            // Save updated data to the rooms.json file
            SaveData();

            Console.WriteLine($"Room {roomNumber} reserved successfully for subject '{subject.SubjectName}'.");
        }
        else
        {
            Console.WriteLine($"Room {roomNumber} not found. Reservation failed.");
        }
    }


    // Add the SaveToFile method
    public void SaveToFile<T>(string fileName, T data)
    {
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(fileName, json);
    }

    public List<T> LoadFromFile<T>(string filePath)
    {
        try
        {
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);
            if (File.Exists(fullPath))
            {
                var json = File.ReadAllText(fullPath);
                Console.WriteLine($"Data loaded from {fullPath}");
                return JsonConvert.DeserializeObject<List<T>>(json);
            }
            Console.WriteLine($"File {fullPath} not found");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading data from {filePath}: {ex.Message}");
        }

        return new List<T>();
    }
}
