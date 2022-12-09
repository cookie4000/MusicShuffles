class Program {
static void Main(string[] args) {    
    
    //
    // Example 1 - Merge
    // 

    // instantiate RNG
    Random random = new Random();
    Song[] songBank = createSongBank();
    Song[] rock =  getGenreArray(songBank,"Rock");
    Song[] classical = getGenreArray(songBank,"Classical");
    Song[] hipHop =  getGenreArray(songBank,"Hip Hop");

    // Do the shuffles on the genre arrays
    Shuffle(rock,random);
    Shuffle(classical,random);
    Shuffle(hipHop,random);

    // Make a collection of Arrays to give to the resizer
    Song[][] genreArrs = new Song[][] {  rock ,  classical ,  hipHop};
    Song[][] spacedArr = resizeArraysAndPosition(genreArrs);
    Song[] shuffledPlaylist = generateFinalPlaylist(spacedArr);

    // Print Playlist Songs to Console
    foreach(Song s in shuffledPlaylist) {
            Console.WriteLine("Artist:{0}   Song:{1}    Genre:{2}", s.artist, s.trackName, s.genre);
    }

    //
    // Example 2 - Function
    // 
    SongPlayer player = new SongPlayer(createSongBank());

    Console.WriteLine("",player.currentSong);;
    Console.WriteLine("Current Song",player.currentSong);
    Console.WriteLine("-------------",player.currentSong);
    Console.WriteLine("Artist:{0}   Song:{1}    Genre:{2}", player.currentSong.artist, 
                                                            player.currentSong.trackName, 
                                                            player.currentSong.genre);
    Console.WriteLine("",player.currentSong);
    Console.WriteLine("Next Song",player.currentSong);
    Console.WriteLine("-------------",player.currentSong);
    player.getNextSong();
    Console.WriteLine("Artist:{0}   Song:{1}    Genre:{2}", player.nextSong.artist, 
                                                            player.nextSong.trackName, 
                                                            player.nextSong.genre);
}

static void Shuffle<T>(T[] array, Random random) {
    // an implementation of the Fisher Yates Shuffle 
    int n = array.Length;
        // loop through the array 
        for (int i = (n - 1); i >=0; i--)
        {   
            // get random position from 0 to i
            int randPos = random.Next(0, i+1);

            // Pick the item
            T selectedItem = array[randPos];
            
            // Swap them
            array[randPos] = array[i];
            array[i] = selectedItem;
        }
}

private static Song[] createSongBank () {

    // Create a playlist of songs which we will shuffle
    Song smokeOnTheWater = new Song("Smoke on the Water", "Deep Purple", "Rock");
    Song bohemianRhapsody = new Song("Bohemian Rhapsody", "Queen", "Rock");
    Song backInBlack = new Song("Back in Black", "AC/DC", "Rock");
    Song thunderstruck = new Song("Thunderstruck", "AC/DC", "Rock");
    Song paranoid = new Song("Paranoid", "Black Sabbath", "Rock");
    Song bornToRun = new Song("Born to Run", "Bruce Springsteen", "Rock");
    Song moonlightSonata = new Song("Moonlight Sonata", "Beethoven", "Classical");
    Song raindrop = new Song("Raindrop Prelude", "Chopin", "Classical");
    Song moonlight = new Song("Concrete Schoolyard", "Jurassic Five", "Hip Hop");
    Song sucker = new Song("Sucker MCs", "Run DMC", "Hip Hop");
    Song fight = new Song("Fight the Power", "Public Enemy", "Hip Hop");

    Song[] songBank = new Song[]{smokeOnTheWater, bohemianRhapsody, backInBlack,thunderstruck,
                                paranoid,bornToRun,moonlightSonata,raindrop,moonlight,sucker,fight};
    
    return songBank;
}

private static Song[] getGenreArray(Song[] songBank, string genre) {
    
    List<Song> selectedSongs = new List<Song>();

    // Loop through the songbank and build an array of songs
    for (int i = 0; i <= songBank.Length -1; i++) {

        Song currentSong = songBank[i];
        if (currentSong.genre == genre) {
            selectedSongs.Add(currentSong);
        }
    }
    return selectedSongs.ToArray();

}

private static Song[][] resizeArraysAndPosition (Song[][] genreSongs) {
    
    List<Song[]> output = new List<Song[]>();

    // Get the largest array length
    int maxLen = getMaxLengthOfGenreArrs(genreSongs);

    // loop through the genre arrays and reposition them
    for (int i = 0; i <= genreSongs.Length -1; i++) {
        Song[] currentArr = genreSongs[i];
        if (currentArr.Length != maxLen) {

           // we need to spread songs
           output.Add(spreadSongs(currentArr,maxLen));
        } 
        else {
            output.Add(currentArr);
        }
    }

    return output.ToArray();
    
}

private static int getMaxLengthOfGenreArrs(Song[][] genreSongs) {

    int maxLen=0;

    // establish which array is the longest 
    for (int i = 0; i <= genreSongs.Length -1; i++) {
        int curLen = genreSongs[i].Length;
        if (curLen > maxLen) {
            maxLen = curLen;
        }
    }

    return maxLen;
}
 
private static Song[] spreadSongs(Song[] input, int maxLen ) {

    Song[] outputArr = new Song[maxLen];
    int noOfSongsToSpace = input.Length;
    int noOfArrayPositions = maxLen;
    int arrPosition = 0;

    decimal div = Decimal.Divide(noOfArrayPositions, noOfSongsToSpace);

    while (div % 1!=0) {
        // we have a remainder. Position an item in the first position and then re-evaluate
        outputArr[arrPosition] = input[arrPosition];
        arrPosition++;
        noOfSongsToSpace--;
        noOfArrayPositions--;
        div = Decimal.Divide(noOfArrayPositions, noOfSongsToSpace);
    }
            
    evenlySpeadItems(input,outputArr,arrPosition,(int)div);


    return outputArr;
}

private static Song[] evenlySpeadItems(Song[] input,Song[] output, int startingPosition, int frequency) {
   
    int countOfSongsMoved = 0;
    int songsToMove = input.Length - startingPosition; 

    
    // Loop through the output array
    for (int i = startingPosition; i <= output.Length -1; i++) {
            // get the song we need to move
            if (songsToMove != countOfSongsMoved) {
                Song currentSong = input[startingPosition + countOfSongsMoved];

                    // if the position in the output array is divisible my frequency - put it there
                    if ((i+1) % frequency==0) { 
                        output[i] = currentSong;
                        countOfSongsMoved++;
                    }
            }
   }

    return output;
  
}

private static Song[] generateFinalPlaylist(Song[][] input) {

    List<Song> playList = new List<Song>();
    int noOfGroups = input.GetUpperBound(0);
    Random random = new Random();

    // Loop through all the array positions
    for (int i = 0; i <= input[0].Length -1; i++) {

        // Make a collection for each column 
        List<Song> columnList = new List<Song>();
        for (int c = 0; c <= noOfGroups; c++) {
            Song currentSong = input[c][i]; 
            if (currentSong is not null) { 
                columnList.Add(currentSong);
            }
        }
        // Shuffle the column
        Song[] columnSongs = columnList.ToArray();
        Shuffle(columnSongs,random);

        // put them onto the output List
        foreach (Song x in columnSongs) {
                playList.Add(x);
        } 
    }
        return playList.ToArray();
    }
}
