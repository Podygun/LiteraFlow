CREATE TABLE IF NOT EXISTS BookGenres
(
    BookGenreId serial primary key,
	Title varchar(64) NOT NULL,
	Description varchar(128) NULL
)