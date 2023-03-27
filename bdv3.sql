--
-- PostgreSQL database dump
--

-- Dumped from database version 14.2
-- Dumped by pg_dump version 14.2

-- Started on 2023-03-26 20:51:53

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 209 (class 1259 OID 41847)
-- Name: User; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."User" (
    id integer NOT NULL,
    login character varying(50)[] NOT NULL,
    password character varying(50)[] NOT NULL,
    name character varying(50)[] NOT NULL,
    surname character varying(50)[] NOT NULL
);


ALTER TABLE public."User" OWNER TO postgres;

--
-- TOC entry 210 (class 1259 OID 41852)
-- Name: User_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."User_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."User_id_seq" OWNER TO postgres;

--
-- TOC entry 3387 (class 0 OID 0)
-- Dependencies: 210
-- Name: User_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."User_id_seq" OWNED BY public."User".id;


--
-- TOC entry 211 (class 1259 OID 41853)
-- Name: academic_plan; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.academic_plan (
    id_academic_plan integer NOT NULL,
    educational_program character varying NOT NULL,
    academic_plan character varying NOT NULL,
    faculty character varying NOT NULL,
    department character varying NOT NULL,
    recruitment_year integer NOT NULL,
    form_of_education character varying(20) NOT NULL,
    training_period integer NOT NULL,
    department_id integer
);


ALTER TABLE public.academic_plan OWNER TO postgres;

--
-- TOC entry 212 (class 1259 OID 41858)
-- Name: academic_plan_id_academic_plan_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.academic_plan_id_academic_plan_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.academic_plan_id_academic_plan_seq OWNER TO postgres;

--
-- TOC entry 3388 (class 0 OID 0)
-- Dependencies: 212
-- Name: academic_plan_id_academic_plan_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.academic_plan_id_academic_plan_seq OWNED BY public.academic_plan.id_academic_plan;


--
-- TOC entry 213 (class 1259 OID 41859)
-- Name: department; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.department (
    id_department integer NOT NULL,
    department character varying NOT NULL,
    abbreviation_d character varying(7),
    name_d character varying NOT NULL,
    head_of_department character varying NOT NULL,
    phone character varying(10)[] NOT NULL,
    e_mail character varying NOT NULL,
    faculty_id integer
);


ALTER TABLE public.department OWNER TO postgres;

--
-- TOC entry 214 (class 1259 OID 41864)
-- Name: department_id_department_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.department_id_department_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.department_id_department_seq OWNER TO postgres;

--
-- TOC entry 3389 (class 0 OID 0)
-- Dependencies: 214
-- Name: department_id_department_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.department_id_department_seq OWNED BY public.department.id_department;


--
-- TOC entry 215 (class 1259 OID 41865)
-- Name: faculty; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.faculty (
    id_faculty integer NOT NULL,
    abbreviation_f character varying(7),
    name_f character varying NOT NULL,
    dukan character varying NOT NULL,
    address character varying NOT NULL,
    phone character varying(10)[] NOT NULL,
    email character varying NOT NULL,
    academic_plan_id integer
);


ALTER TABLE public.faculty OWNER TO postgres;

--
-- TOC entry 216 (class 1259 OID 41870)
-- Name: faculty_id_faculty_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.faculty_id_faculty_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.faculty_id_faculty_seq OWNER TO postgres;

--
-- TOC entry 3390 (class 0 OID 0)
-- Dependencies: 216
-- Name: faculty_id_faculty_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.faculty_id_faculty_seq OWNED BY public.faculty.id_faculty;


--
-- TOC entry 217 (class 1259 OID 41871)
-- Name: group; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."group" (
    id integer NOT NULL,
    name character varying(20)[] NOT NULL,
    recruitment_year integer NOT NULL
);


ALTER TABLE public."group" OWNER TO postgres;

--
-- TOC entry 218 (class 1259 OID 41876)
-- Name: group_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.group_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.group_id_seq OWNER TO postgres;

--
-- TOC entry 3391 (class 0 OID 0)
-- Dependencies: 218
-- Name: group_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.group_id_seq OWNED BY public."group".id;


--
-- TOC entry 219 (class 1259 OID 41877)
-- Name: head_of_the_enterprise; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.head_of_the_enterprise (
    id integer NOT NULL,
    phone character varying(10)[] NOT NULL,
    org_id integer[]
);


ALTER TABLE public.head_of_the_enterprise OWNER TO postgres;

--
-- TOC entry 220 (class 1259 OID 41882)
-- Name: head_of_the_enterprise_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.head_of_the_enterprise_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.head_of_the_enterprise_id_seq OWNER TO postgres;

--
-- TOC entry 3392 (class 0 OID 0)
-- Dependencies: 220
-- Name: head_of_the_enterprise_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.head_of_the_enterprise_id_seq OWNED BY public.head_of_the_enterprise.id;


--
-- TOC entry 221 (class 1259 OID 41883)
-- Name: head_of_the_university; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.head_of_the_university (
    id integer NOT NULL,
    name character varying[] NOT NULL,
    surname character varying[] NOT NULL,
    patranomic character varying[],
    post character varying[] NOT NULL,
    "level of education" character varying[] NOT NULL,
    "Academic degree" character varying[],
    "work experience" character varying[]
);


ALTER TABLE public.head_of_the_university OWNER TO postgres;

--
-- TOC entry 222 (class 1259 OID 41888)
-- Name: students; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.students (
    id integer NOT NULL,
    name character varying(50)[] NOT NULL,
    surname character varying(50) NOT NULL,
    patranomic character varying(50)[] NOT NULL,
    date_of_birtf date NOT NULL,
    "N_record_book" integer NOT NULL,
    group_id integer NOT NULL
);


ALTER TABLE public.students OWNER TO postgres;

--
-- TOC entry 223 (class 1259 OID 41893)
-- Name: students_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.students_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.students_id_seq OWNER TO postgres;

--
-- TOC entry 3393 (class 0 OID 0)
-- Dependencies: 223
-- Name: students_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.students_id_seq OWNED BY public.students.id;


--
-- TOC entry 3198 (class 2604 OID 41894)
-- Name: User id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."User" ALTER COLUMN id SET DEFAULT nextval('public."User_id_seq"'::regclass);


--
-- TOC entry 3199 (class 2604 OID 41895)
-- Name: academic_plan id_academic_plan; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.academic_plan ALTER COLUMN id_academic_plan SET DEFAULT nextval('public.academic_plan_id_academic_plan_seq'::regclass);


--
-- TOC entry 3200 (class 2604 OID 41896)
-- Name: department id_department; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.department ALTER COLUMN id_department SET DEFAULT nextval('public.department_id_department_seq'::regclass);


--
-- TOC entry 3201 (class 2604 OID 41897)
-- Name: faculty id_faculty; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.faculty ALTER COLUMN id_faculty SET DEFAULT nextval('public.faculty_id_faculty_seq'::regclass);


--
-- TOC entry 3202 (class 2604 OID 41898)
-- Name: group id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."group" ALTER COLUMN id SET DEFAULT nextval('public.group_id_seq'::regclass);


--
-- TOC entry 3203 (class 2604 OID 41899)
-- Name: head_of_the_enterprise id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.head_of_the_enterprise ALTER COLUMN id SET DEFAULT nextval('public.head_of_the_enterprise_id_seq'::regclass);


--
-- TOC entry 3204 (class 2604 OID 41900)
-- Name: students id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.students ALTER COLUMN id SET DEFAULT nextval('public.students_id_seq'::regclass);


--
-- TOC entry 3367 (class 0 OID 41847)
-- Dependencies: 209
-- Data for Name: User; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3369 (class 0 OID 41853)
-- Dependencies: 211
-- Data for Name: academic_plan; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3371 (class 0 OID 41859)
-- Dependencies: 213
-- Data for Name: department; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3373 (class 0 OID 41865)
-- Dependencies: 215
-- Data for Name: faculty; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3375 (class 0 OID 41871)
-- Dependencies: 217
-- Data for Name: group; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3377 (class 0 OID 41877)
-- Dependencies: 219
-- Data for Name: head_of_the_enterprise; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3379 (class 0 OID 41883)
-- Dependencies: 221
-- Data for Name: head_of_the_university; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3380 (class 0 OID 41888)
-- Dependencies: 222
-- Data for Name: students; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3394 (class 0 OID 0)
-- Dependencies: 210
-- Name: User_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."User_id_seq"', 1, false);


--
-- TOC entry 3395 (class 0 OID 0)
-- Dependencies: 212
-- Name: academic_plan_id_academic_plan_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.academic_plan_id_academic_plan_seq', 1, false);


--
-- TOC entry 3396 (class 0 OID 0)
-- Dependencies: 214
-- Name: department_id_department_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.department_id_department_seq', 1, false);


--
-- TOC entry 3397 (class 0 OID 0)
-- Dependencies: 216
-- Name: faculty_id_faculty_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.faculty_id_faculty_seq', 1, false);


--
-- TOC entry 3398 (class 0 OID 0)
-- Dependencies: 218
-- Name: group_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.group_id_seq', 1, false);


--
-- TOC entry 3399 (class 0 OID 0)
-- Dependencies: 220
-- Name: head_of_the_enterprise_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.head_of_the_enterprise_id_seq', 1, false);


--
-- TOC entry 3400 (class 0 OID 0)
-- Dependencies: 223
-- Name: students_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.students_id_seq', 1, false);


--
-- TOC entry 3206 (class 2606 OID 41902)
-- Name: User User_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."User"
    ADD CONSTRAINT "User_pkey" PRIMARY KEY (id);


--
-- TOC entry 3208 (class 2606 OID 41904)
-- Name: academic_plan academic_plan_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.academic_plan
    ADD CONSTRAINT academic_plan_pkey PRIMARY KEY (id_academic_plan);


--
-- TOC entry 3210 (class 2606 OID 41906)
-- Name: department department_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.department
    ADD CONSTRAINT department_pkey PRIMARY KEY (id_department);


--
-- TOC entry 3212 (class 2606 OID 41908)
-- Name: faculty faculty_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.faculty
    ADD CONSTRAINT faculty_pkey PRIMARY KEY (id_faculty);


--
-- TOC entry 3214 (class 2606 OID 41910)
-- Name: group group_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."group"
    ADD CONSTRAINT group_pkey PRIMARY KEY (id);


--
-- TOC entry 3216 (class 2606 OID 41912)
-- Name: head_of_the_enterprise head_of_the_enterprise_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.head_of_the_enterprise
    ADD CONSTRAINT head_of_the_enterprise_pkey PRIMARY KEY (id);


--
-- TOC entry 3220 (class 2606 OID 41914)
-- Name: students students_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.students
    ADD CONSTRAINT students_pkey PRIMARY KEY (id);


--
-- TOC entry 3218 (class 2606 OID 41916)
-- Name: head_of_the_university teacher_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.head_of_the_university
    ADD CONSTRAINT teacher_pkey PRIMARY KEY (id);


--
-- TOC entry 3221 (class 2606 OID 41917)
-- Name: academic_plan academic_plan_department_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.academic_plan
    ADD CONSTRAINT academic_plan_department_id_fkey FOREIGN KEY (department_id) REFERENCES public.department(id_department);


--
-- TOC entry 3222 (class 2606 OID 41922)
-- Name: department department_faculty_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.department
    ADD CONSTRAINT department_faculty_id_fkey FOREIGN KEY (faculty_id) REFERENCES public.faculty(id_faculty);


--
-- TOC entry 3223 (class 2606 OID 41927)
-- Name: faculty faculty_academic_plan_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.faculty
    ADD CONSTRAINT faculty_academic_plan_id_fkey FOREIGN KEY (academic_plan_id) REFERENCES public.academic_plan(id_academic_plan);


--
-- TOC entry 3224 (class 2606 OID 41932)
-- Name: head_of_the_enterprise head_of_the_enterprise_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.head_of_the_enterprise
    ADD CONSTRAINT head_of_the_enterprise_id_fkey FOREIGN KEY (id) REFERENCES public."User"(id) NOT VALID;


--
-- TOC entry 3225 (class 2606 OID 41937)
-- Name: head_of_the_university head_of_the_university_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.head_of_the_university
    ADD CONSTRAINT head_of_the_university_id_fkey FOREIGN KEY (id) REFERENCES public."User"(id) NOT VALID;


--
-- TOC entry 3226 (class 2606 OID 41942)
-- Name: students students_group_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.students
    ADD CONSTRAINT students_group_id_fkey FOREIGN KEY (group_id) REFERENCES public."group"(id) NOT VALID;


--
-- TOC entry 3227 (class 2606 OID 41947)
-- Name: students students_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.students
    ADD CONSTRAINT students_id_fkey FOREIGN KEY (id) REFERENCES public."User"(id) NOT VALID;


-- Completed on 2023-03-26 20:51:53

--
-- PostgreSQL database dump complete
--

