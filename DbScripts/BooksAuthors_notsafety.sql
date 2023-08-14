create table if not exists BooksAuthors 
(
	BookAuthor serial primary key,
	BookId int references Books (BookId) on update cascade on delete cascade,
	ProfileId int references Profiles (ProfileId) on update cascade on delete set null
)