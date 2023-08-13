CREATE TABLE IF NOT EXISTS BooksTags
(
    BookTagId serial primary key,
	BookId int references Books (BookId) on update cascade on delete cascade,
	TagId int references Tags (TagId) on update cascade on delete cascade
)