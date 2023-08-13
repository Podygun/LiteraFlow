CREATE TABLE IF NOT EXISTS Chapters
(
    ChapterId serial primary key,
	Title varchar(64) NOT NULL,
	"text" text NOT NULL,
	BookId int references Books (BookId) on update cascade on delete cascade,
	updatedon timestamp
)