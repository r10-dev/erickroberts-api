drop table er.user;

CREATE TABLE er.user (
    userid serial,
    usernname varchar(256) not null,
    constraint user_pkey PRIMARY KEY (userid)
)