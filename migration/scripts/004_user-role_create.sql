drop table er.userrole;


create table er.userrole (
    user_roleid serial,
    userid int not null,
    constraint user_role_pkey primary key (user_roleid)
)