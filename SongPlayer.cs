public class SongPlayer {
    public Song[] songBankAll;
    public Song[] songBankFiltered;
    public Song currentSong;
    public Song? nextSong;


    public SongPlayer(Song[] songBank) {
        this.songBankAll = songBank;
        this.songBankFiltered = songBank;
        this.currentSong = getrandomSongFromBank();
    }

    public void getNextSong() {

        bool isValidSong = false;   
        while (isValidSong == false) {

            // Select Random Song
            Song currentSelection = getrandomSongFromBank();

            // validate 
            bool isValid = isValidSongChoice(currentSelection,this.currentSong);

            if (isValid) {
                this.nextSong = currentSelection;
                break;
            }

            // if there is just one song left - play that
            if (this.songBankFiltered.Length ==1) {
                this.nextSong = currentSelection;
                break;
            }

        }

    }

    private Song getrandomSongFromBank(){
        Random random = new Random();
        int selection = random.Next(0, this.songBankFiltered.GetUpperBound(0));

        return this.songBankFiltered[selection];

    }

    private bool isValidSongChoice(Song nextSong, Song currentSong) {
        if (nextSong.artist != currentSong.artist && nextSong.genre!=currentSong.genre) {
            this.songBankFiltered = this.songBankAll;
            return true;
        }

        // Remove song from selection 
        removeSongFromFilteredList(nextSong);
        return false;
    }

    private void removeSongFromFilteredList(Song songToRemove) {

        // Loop through the filtered list and remove where you find the song to remove
        for (int i = 0; i <= this.songBankFiltered.Length -1; i++) {
            if (this.songBankFiltered[i].artist == songToRemove.artist && 
                this.songBankFiltered[i].trackName == songToRemove.trackName) {
                    
                List<Song> songBankList = this.songBankFiltered.ToList();
                songBankList.RemoveAt(i);
                this.songBankFiltered = songBankList.ToArray();

            }
        }
    }

}