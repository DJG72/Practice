using System;
using System.Collections.Generic;

/// <summary>
/// Singleton Music Player Class
/// </summary>
class MusicPlayer
{
    // Single instance of MusicPlayer
    private static readonly MusicPlayer instance = new MusicPlayer();

    // Playlist
    private List<string> songs;

    // Currently playing song
    private string currentSong;

    // Private constructor prevents direct object creation
    private MusicPlayer()
    {
        songs = new List<string>();
        currentSong = null;
    }

    // Public method to get the single instance
    public static MusicPlayer GetInstance()
    {
        return instance;
    }

    // Add song to playlist
    public void AddSong(string song)
    {
        songs.Add(song);
        Console.WriteLine($"'{song}' added to playlist.");
    }

    // Play a song
    public void PlaySong(string song)
    {
        if (songs.Contains(song))
        {
            currentSong = song;
            Console.WriteLine($"Now Playing: {song}");
        }
        else
        {
            Console.WriteLine($"'{song}' not found in playlist.");
        }
    }

    // Pause current song
    public void PauseSong()
    {
        if (currentSong != null)
        {
            Console.WriteLine($"'{currentSong}' paused.");
        }
        else
        {
            Console.WriteLine("No song is currently playing.");
        }
    }

    // Show playlist
    public void ShowPlaylist()
    {
        Console.WriteLine("\n===== Playlist =====");

        if (songs.Count == 0)
        {
            Console.WriteLine("Playlist is empty.");
            return;
        }

        foreach (string song in songs)
        {
            Console.WriteLine("- " + song);
        }
    }

    // Show currently playing song
    public void ShowCurrentSong()
    {
        Console.WriteLine("\n===== Current Song =====");

        if (currentSong == null)
        {
            Console.WriteLine("No song is currently playing.");
        }
        else
        {
            Console.WriteLine("Now Playing: " + currentSong);
        }
    }
}

class SingletonPattern
{
    static void Main()
    {
        // Get Singleton instance
        MusicPlayer player1 = MusicPlayer.GetInstance();

        // Get another reference to prove Singleton
        MusicPlayer player2 = MusicPlayer.GetInstance();

        Console.WriteLine("Singleton Demonstration");
        Console.WriteLine("-----------------------");
        Console.WriteLine($"player1 and player2 refer to same object: " +
                          $"{Object.ReferenceEquals(player1, player2)}");

        int choice;

        do
        {
            Console.WriteLine("\n===== MUSIC PLAYER MENU =====");
            Console.WriteLine("1. Add Song");
            Console.WriteLine("2. Play Song");
            Console.WriteLine("3. Pause Song");
            Console.WriteLine("4. Show Playlist");
            Console.WriteLine("5. Show Current Song");
            Console.WriteLine("6. Exit");
            Console.Write("Enter your choice: ");

            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input. Enter a number.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    Console.Write("Enter song name: ");
                    string addSong = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(addSong))
                    {
                        player1.AddSong(addSong);
                    }
                    else
                    {
                        Console.WriteLine("Song name cannot be empty.");
                    }
                    break;

                case 2:
                    Console.Write("Enter song to play: ");
                    string playSong = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(playSong))
                    {
                        player2.PlaySong(playSong);
                    }
                    else
                    {
                        Console.WriteLine("Song name cannot be empty.");
                    }
                    break;

                case 3:
                    player1.PauseSong();
                    break;

                case 4:
                    player2.ShowPlaylist();
                    break;

                case 5:
                    player1.ShowCurrentSong();
                    break;

                case 6:
                    Console.WriteLine("Thank you for using Music Player.");
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

        } while (choice != 6);
    }
}