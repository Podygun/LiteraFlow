create table if not exists UserTokens(
	UserTokenId uuid primary key,
	UserId int REFERENCES Users(UserId),
	CreatedAt timestamp
)