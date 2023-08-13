CREATE TABLE IF NOT EXISTS BookTypes
(
    BookTypeId serial primary key,
	Title varchar(32) NOT NULL,
	Description varchar(128) NULL
)