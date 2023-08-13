CREATE TABLE IF NOT EXISTS Books
(
    BookId serial primary key,
	Title varchar(64) NOT NULL,
	TypeId int references BookTypes (BookTypeId) on update cascade on delete restrict,
	GenreId int references BookGenres (BookGenreId) on update cascade on delete set null,
	AuthorNote varchar(500),
	Description varchar(500),
	IsAdultContent bool,
	CreatedOn timestamp,
	WhoCanWatch int references PermissionTypes(PermissionTypeId) on update cascade on delete restrict,
	WhoCanDownload int references PermissionTypes(PermissionTypeId) on update cascade on delete restrict,
	WhoCanComment int references PermissionTypes(PermissionTypeId) on update cascade on delete restrict,
	AmountUnlockedChapters int null,
	BookImage varchar (128),
	Price decimal(5,2)
)