drop table er.userinfo;

CREATE TABLE er.userinfo (
    userinfoid serial,
    userid int not null,
    passHash UUID not null,
    secretKey UUID not null,
    email varchar(256) not null,
    constraint userinfo_pkey PRIMARY KEY (userinfoid)
)