drop table er.role;

create table er.role (
    roleid serial,
    title varchar(256),
    permissions JSON,
    constraint role_pkey primary key (roleid)
)