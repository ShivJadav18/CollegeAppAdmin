-- CREATE TABLE ResetToken (
--     token_id SERIAL PRIMARY KEY,
--     user_id INT REFERENCES "user"(user_id),
--     token VARCHAR(255) UNIQUE,
--     isReseted BOOLEAN DEFAULT FALSE,
--     CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
--     ExpiredAt TIMESTAMP
-- );

-- CREATE TABLE Unit (
--     unit_id SERIAL PRIMARY KEY,
--     name VARCHAR(50),
--     shortName VARCHAR(20) UNIQUE,
-- 	CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
--     UpdatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
--     CreatedBy INT REFERENCES "user"(user_id),
--     UpdatedBy INT REFERENCES "user"(user_id)
-- );
-- alter table resettoken drop constraint resettoken_user_id_fkey;
-- alter table resettoken add constraint resettoken_user_id_fkey foreign key (user_id) references "user"(user_id) on delete cascade on update cascade;


-- alter table permission drop constraint Permission_permissionfield_id_fkey;
-- alter table permission add constraint Permission_permissionfield_id_fkey foreign key (permissionfield_id) references permissionfield(permissionfield_id) on delete cascade on update cascade;

-- alter table permission drop constraint permission_role_id_fkey;
-- alter table permission add constraint permission_role_id_fkey foreign key (role_id) references role(role_id) on delete cascade on update cascade;


-- alter table modifiertomodifiergroup drop constraint modifiertomodifiergroup_modifier_id_fkey;
-- alter table modifiertomodifiergroup add constraint modifiertomodifiergroup_modifier_id_fkey foreign key (modifier_id) references modifier(modifier_id) on delete cascade on update cascade;

-- alter table modifiertomodifiergroup drop constraint modifiertomodifiergroup_modifiergroup_id_fkey;
-- alter table modifiertomodifiergroup add constraint modifiertomodifiergroup_modifiergroup_id_fkey foreign key (modifiergroup_id) references modifiergroup(modifiergroup_id) on delete cascade on update cascade;


-- alter table itemtomodifiergroup drop constraint itemtomodifiergroup_item_id_fkey;
-- alter table itemtomodifiergroup add constraint itemtomodifiergroup_item_id_fkey foreign key (item_id) references item(item_id) on delete cascade on update cascade;

-- alter table itemtomodifiergroup drop constraint itemtomodifiergroup_modifiergroup_id_fkey;
-- alter table itemtomodifiergroup add constraint itemtomodifiergroup_modifiergroup_id_fkey foreign key (modifiergroup_id) references modifiergroup(modifiergroup_id) on delete cascade on update cascade;


-- alter table "table" drop constraint table_section_id_fkey;
-- alter table "table" add constraint table_section_id_fkey foreign key (section_id) references section(section_id) on delete cascade on update cascade;


-- alter table favouriteitem drop constraint favouriteitem_item_id_fkey;
-- alter table favouriteitem add constraint favouriteitem_item_id_fkey foreign key (item_id) references item(item_id) on delete cascade on update cascade;


-- alter table ordertotable drop constraint ordertotable_order_id_fkey;
-- alter table ordertotable add constraint ordertotable_order_id_fkey foreign key (order_id) references "order"(order_id) on delete cascade on update cascade;


-- alter table ordertoitem drop constraint ordertoitem_item_id_fkey;
-- alter table ordertoitem add constraint ordertoitem_item_id_fkey foreign key (item_id) references item(item_id) on delete cascade on update cascade;

-- alter table ordertoitem drop constraint ordertoitem_order_id_fkey;
-- alter table ordertoitem add constraint ordertoitem_order_id_fkey foreign key (order_id) references "order"(order_id) on delete cascade on update cascade;


-- alter table orderitemmodifier drop constraint orderitemmodifier_modifier_id_fkey;
-- alter table orderitemmodifier add constraint orderitemmodifier_modifier_id_fkey foreign key (modifier_id) references modifier(modifier_id) on delete cascade on update cascade;

-- alter table orderitemmodifier drop constraint orderitemmodifier_ordertoitem_id_fkey;
-- alter table orderitemmodifier add constraint orderitemmodifier_ordertoitem_id_fkey foreign key (ordertoitem_id) references ordertoitem(ordertoitem_id) on delete cascade on update cascade;


-- alter table waitingtoken drop constraint waitingtoken_section_id_fkey;
-- alter table waitingtoken add constraint waitingtoken_section_id_fkey foreign key (section_id) references section(section_id) on delete cascade on update cascade;


-- alter table payment drop constraint payment_order_id_fkey;
-- alter table payment add constraint payment_order_id_fkey foreign key (order_id) references "order"(order_id) on delete cascade on update cascade;


-- alter table rating drop constraint rating_order_id_fkey;
-- alter table rating add constraint rating_order_id_fkey foreign key (order_id) references "order"(order_id) on delete cascade on update cascade;
