\connect hrinventorydb;

/* Define getutcdate function */
-- Table: public.catagory

-- DROP TABLE public.catagory;

CREATE TABLE public.catagory
(
    categoryid serial NOT NULL,
    categoryname character varying(50) COLLATE pg_catalog."default" NOT NULL,
    categorydescription character varying(250) COLLATE pg_catalog."default" NOT NULL,
    userid character varying(50) COLLATE pg_catalog."default" NOT NULL,
    createddate timestamp without time zone NOT NULL,
    isdeleted character varying(50) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT catagory_pkey PRIMARY KEY (categoryid)
);


ALTER TABLE public.catagory
    OWNER to postgres;

-- Table: public.product

-- DROP TABLE public.product;

CREATE TABLE public.product
(
    productid serial NOT NULL,
    categoryid bigint NOT NULL,
    productname character varying(50) COLLATE pg_catalog."default" NOT NULL,
    productdescription character varying(250) COLLATE pg_catalog."default" NOT NULL,
    userid character varying(50) COLLATE pg_catalog."default" NOT NULL,
    createddate timestamp without time zone NOT NULL,
    isdeleted character varying(50) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT product_pkey PRIMARY KEY (productid),
    CONSTRAINT product_categoryid_fkey FOREIGN KEY (categoryid)
        REFERENCES public.catagory (categoryid) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);



ALTER TABLE public.product
    OWNER to postgres;


-- Table: public.pomaster

-- DROP TABLE public.pomaster;

CREATE TABLE public.pomaster
(
    poid serial NOT NULL,
    podate timestamp without time zone NOT NULL,
    totalamount double precision NOT NULL,
    discount double precision NOT NULL,
    finalamount double precision NOT NULL,
    userid character varying(50) COLLATE pg_catalog."default" NOT NULL,
    createddate timestamp without time zone NOT NULL,
    isdeleted character varying(50) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT pomaster_pkey PRIMARY KEY (poid)
);



ALTER TABLE public.pomaster
    OWNER to postgres;



-- Table: public.podetail

-- DROP TABLE public.podetail;

CREATE TABLE public.podetail
(
    podetailid serial NOT NULL,
    poid bigint NOT NULL,
    productid bigint NOT NULL,
    quantity bigint NOT NULL,
    porate double precision NOT NULL,
    discount double precision NOT NULL,
    amount double precision NOT NULL,
    userid character varying(50) COLLATE pg_catalog."default" NOT NULL,
    createddate timestamp without time zone NOT NULL,
    isdeleted character varying(50) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT podetail_pkey PRIMARY KEY (podetailid),
    CONSTRAINT podetail_poid_fkey FOREIGN KEY (poid)
        REFERENCES public.pomaster (poid) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT podetail_productid_fkey FOREIGN KEY (productid)
        REFERENCES public.product (productid) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);



ALTER TABLE public.podetail
    OWNER to postgres;



-- Table: public.dispatchmaster

-- DROP TABLE public.dispatchmaster;

CREATE TABLE public.dispatchmaster
(
    dispatchid serial NOT NULL ,
    dispatchdate timestamp without time zone NOT NULL,
    employeeid bigint NOT NULL,
    employeename character varying(50) COLLATE pg_catalog."default" NOT NULL,
    totalqty bigint NOT NULL,
    userid character varying(50) COLLATE pg_catalog."default" NOT NULL,
    createddate timestamp without time zone NOT NULL,
    isdeleted character varying(50) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT dispatchmaster_pkey PRIMARY KEY (dispatchid)
)



ALTER TABLE public.dispatchmaster
    OWNER to postgres;


-- Table: public.dispatchdetails

-- DROP TABLE public.dispatchdetails;

CREATE TABLE public.dispatchdetails
(
    dispatchdetailid serial NOT NULL ,
    dispatchid integer NOT NULL,
    productid bigint NOT NULL,
    quantity bigint NOT NULL,
    userid character varying(50) COLLATE pg_catalog."default" NOT NULL,
    createddate timestamp without time zone NOT NULL,
    isdeleted character varying(50) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT dispatchdetails_pkey PRIMARY KEY (dispatchdetailid),
    CONSTRAINT dispatchdetails_dispatchid_fkey FOREIGN KEY (dispatchid)
        REFERENCES public.dispatchmaster (dispatchid) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)



ALTER TABLE public.dispatchdetails
    OWNER to postgres;
	
	
	
	-- Table: public.reqestmaster

-- DROP TABLE public.reqestmaster;

CREATE TABLE public.reqestmaster
(
    requestid serial NOT NULL ,
    employeeid character varying(50) COLLATE pg_catalog."default",
    isread boolean NOT NULL,
    userid character varying(50) COLLATE pg_catalog."default",
    createddate timestamp with time zone NOT NULL,
    isdeleted character varying(50) COLLATE pg_catalog."default",
    CONSTRAINT reqestmaster_pkey PRIMARY KEY (requestid)
)



ALTER TABLE public.reqestmaster
    OWNER to postgres;
	
	
	
	-- Table: public.requestdetail

-- DROP TABLE public.requestdetail;

CREATE TABLE public.requestdetail
(
    requestdetailid serial NOT NULL ,
    requestid integer NOT NULL,
    productid integer NOT NULL,
    quantity bigint NOT NULL,
    status character varying(50) COLLATE pg_catalog."default",
    userid character varying(50) COLLATE pg_catalog."default",
    createddate timestamp with time zone NOT NULL,
    isdeleted character varying(50) COLLATE pg_catalog."default",
    CONSTRAINT requestdetail_pkey PRIMARY KEY (requestdetailid),
    CONSTRAINT requestdetail_productid_fkey FOREIGN KEY (productid)
        REFERENCES public.product (productid) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT requestdetail_requestid_fkey FOREIGN KEY (requestid)
        REFERENCES public.reqestmaster (requestid) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)



ALTER TABLE public.requestdetail
    OWNER to postgres;
	
	
	
	-- View: public.podispatchdetailsgrid

-- DROP VIEW public.podispatchdetailsgrid;

CREATE OR REPLACE VIEW public.podispatchdetailsgrid
 AS
 SELECT derived.productid,
    sum(derived.amount) AS balance
   FROM ( SELECT podetail.productid,
            sum(podetail.quantity) AS amount
           FROM podetail
          WHERE podetail.isdeleted::text = 'false'::text
          GROUP BY podetail.productid
        UNION ALL
         SELECT dispatchdetails.productid,
            - sum(dispatchdetails.quantity) AS amount
           FROM dispatchdetails
          WHERE dispatchdetails.isdeleted::text = 'false'::text
          GROUP BY dispatchdetails.productid) derived
  GROUP BY derived.productid;

ALTER TABLE public.podispatchdetailsgrid
    OWNER TO postgres;
