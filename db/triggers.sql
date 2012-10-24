--------------------------------------------------------
-- Archivo creado  - sábado-octubre-20-2012   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Trigger SUPPLIERS_INS_TRG
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "OSMAR_USER"."SUPPLIERS_INS_TRG" 
	before insert on suppliers
	for each row
	begin 
		select suppliers_seq.nextval into :new.id from dual;
	end;
/
ALTER TRIGGER "OSMAR_USER"."SUPPLIERS_INS_TRG" ENABLE;
--------------------------------------------------------
--  DDL for Trigger REGIONS_INS_TRG
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "OSMAR_USER"."REGIONS_INS_TRG" 
	before insert on regions
	for each row
	begin
		select regions_seq.nextval into :new.id from dual;
	end;
/
ALTER TRIGGER "OSMAR_USER"."REGIONS_INS_TRG" ENABLE;
--------------------------------------------------------
--  DDL for Trigger PRODUCTS_INS_TRG
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "OSMAR_USER"."PRODUCTS_INS_TRG" 
	before insert on products
	for each row
	begin
		select products_seq.nextval into :new.id from dual;
	end;
/
ALTER TRIGGER "OSMAR_USER"."PRODUCTS_INS_TRG" ENABLE;
--------------------------------------------------------
--  DDL for Trigger CUSTOMERS_INS_TRG
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "OSMAR_USER"."CUSTOMERS_INS_TRG" 
	before insert on customers
	for each row
	begin
		select customers_seq.nextval into :new.id from dual;
	end;
/
ALTER TRIGGER "OSMAR_USER"."CUSTOMERS_INS_TRG" ENABLE;
--------------------------------------------------------
--  DDL for Trigger ORDERS_INS_TRG
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "OSMAR_USER"."ORDERS_INS_TRG" 
	before insert on orders
	for each row
	begin
		select orders_seq.nextval into :new.id from dual;
	end;
/
ALTER TRIGGER "OSMAR_USER"."ORDERS_INS_TRG" ENABLE;
--------------------------------------------------------
--  DDL for Trigger ORDER_LINES_INS_TRG
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "OSMAR_USER"."ORDER_LINES_INS_TRG" 
	before insert on order_lines
	for each row
	begin
		select order_lines_seq.nextval into :new.id from dual;
	end;
/
ALTER TRIGGER "OSMAR_USER"."ORDER_LINES_INS_TRG" ENABLE;
--------------------------------------------------------
--  DDL for Trigger INCIDENTS_INS_TRG
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "OSMAR_USER"."INCIDENTS_INS_TRG" 
	before insert on incidents
	for each row
	begin	
		select incidents_seq.nextval into :new.id from dual;
	end;
/
ALTER TRIGGER "OSMAR_USER"."INCIDENTS_INS_TRG" ENABLE;
--------------------------------------------------------
--  DDL for Trigger EMPLOYEES_INS_TRG
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "OSMAR_USER"."EMPLOYEES_INS_TRG" 
	before insert on employees
	for each row
	begin
		select employees_seq.nextval into :new.id from dual;
	end;
/
ALTER TRIGGER "OSMAR_USER"."EMPLOYEES_INS_TRG" ENABLE;
--------------------------------------------------------
--  DDL for Trigger DEPOTS_INS_TRG
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "OSMAR_USER"."DEPOTS_INS_TRG" 
	before insert on depots
	for each row
	begin
		select depots_seq.nextval into :new.id from dual;
	end;
/
ALTER TRIGGER "OSMAR_USER"."DEPOTS_INS_TRG" ENABLE;
--------------------------------------------------------
--  DDL for Trigger DEPOT_PRODUCTS_INS_TRG
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "OSMAR_USER"."DEPOT_PRODUCTS_INS_TRG" 
	before insert on depot_products
	for each row
	begin
		select depot_products_seq.nextval into :new.id from dual;	
	end;
/
ALTER TRIGGER "OSMAR_USER"."DEPOT_PRODUCTS_INS_TRG" ENABLE;
--------------------------------------------------------
--  DDL for Trigger USERS_INS_TRG
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "OSMAR_USER"."USERS_INS_TRG" 
	before insert on users
	for each row
	begin
		select users_seq.nextval into :new.id from dual;
	end;
/
ALTER TRIGGER "OSMAR_USER"."USERS_INS_TRG" ENABLE;
--------------------------------------------------------
--  DDL for Trigger COUNTRIES_INS_TRG
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "OSMAR_USER"."COUNTRIES_INS_TRG" 
	before insert on countries
	for each row
	begin
		select countries_seq.nextval into :new.id from dual;
	end;
/
ALTER TRIGGER "OSMAR_USER"."COUNTRIES_INS_TRG" ENABLE;
