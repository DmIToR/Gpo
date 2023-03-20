--
-- PostgreSQL database dump
--

-- Dumped from database version 14.2
-- Dumped by pg_dump version 14.2

-- Started on 2023-03-20 20:36:03

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
-- TOC entry 213 (class 1259 OID 41567)
-- Name: academic_plan; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.academic_plan (
    id_academic_plan integer NOT NULL,
    educational_program text NOT NULL,
    academic_plan text NOT NULL,
    faculty text NOT NULL,
    department text NOT NULL,
    recruitment_year integer NOT NULL,
    form_of_education character varying(20) NOT NULL,
    training_period integer NOT NULL,
    department_id integer
);


ALTER TABLE public.academic_plan OWNER TO postgres;

--
-- TOC entry 212 (class 1259 OID 41566)
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
-- TOC entry 3358 (class 0 OID 0)
-- Dependencies: 212
-- Name: academic_plan_id_academic_plan_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.academic_plan_id_academic_plan_seq OWNED BY public.academic_plan.id_academic_plan;


--
-- TOC entry 217 (class 1259 OID 41590)
-- Name: department; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.department (
    id_department integer NOT NULL,
    department text NOT NULL,
    abbreviation_d character varying(7),
    name_d text NOT NULL,
    head_of_department text NOT NULL,
    phone integer NOT NULL,
    e_mail text NOT NULL,
    faculty_id integer
);


ALTER TABLE public.department OWNER TO postgres;

--
-- TOC entry 216 (class 1259 OID 41589)
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
-- TOC entry 3359 (class 0 OID 0)
-- Dependencies: 216
-- Name: department_id_department_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.department_id_department_seq OWNED BY public.department.id_department;


--
-- TOC entry 215 (class 1259 OID 41576)
-- Name: faculty; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.faculty (
    id_faculty integer NOT NULL,
    abbreviation_f character varying(7),
    name_f text NOT NULL,
    dukan text NOT NULL,
    address text NOT NULL,
    phone integer NOT NULL,
    email text NOT NULL,
    academic_plan_id integer
);


ALTER TABLE public.faculty OWNER TO postgres;

--
-- TOC entry 214 (class 1259 OID 41575)
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
-- TOC entry 3360 (class 0 OID 0)
-- Dependencies: 214
-- Name: faculty_id_faculty_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.faculty_id_faculty_seq OWNED BY public.faculty.id_faculty;


--
-- TOC entry 210 (class 1259 OID 41538)
-- Name: group; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."group" (
    id integer[] NOT NULL,
    name character varying[] NOT NULL,
    "recruitment year" date NOT NULL
);


ALTER TABLE public."group" OWNER TO postgres;

--
-- TOC entry 209 (class 1259 OID 41531)
-- Name: students; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.students (
    id integer[] NOT NULL,
    name character varying[] NOT NULL,
    surname character varying[] NOT NULL,
    patronymic character varying[],
    date_birth date[] NOT NULL,
    "record book" character varying[],
    group_id integer[] NOT NULL
);


ALTER TABLE public.students OWNER TO postgres;

--
-- TOC entry 211 (class 1259 OID 41550)
-- Name: teacher; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.teacher (
    id integer NOT NULL,
    name character varying[] NOT NULL,
    surname character varying[] NOT NULL,
    patranomic character varying[],
    post character varying[] NOT NULL,
    "level of education" character varying[] NOT NULL,
    "Academic degree" character varying[],
    "work experience" character varying[]
);


ALTER TABLE public.teacher OWNER TO postgres;

--
-- TOC entry 3186 (class 2604 OID 41570)
-- Name: academic_plan id_academic_plan; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.academic_plan ALTER COLUMN id_academic_plan SET DEFAULT nextval('public.academic_plan_id_academic_plan_seq'::regclass);


--
-- TOC entry 3188 (class 2604 OID 41593)
-- Name: department id_department; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.department ALTER COLUMN id_department SET DEFAULT nextval('public.department_id_department_seq'::regclass);


--
-- TOC entry 3187 (class 2604 OID 41579)
-- Name: faculty id_faculty; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.faculty ALTER COLUMN id_faculty SET DEFAULT nextval('public.faculty_id_faculty_seq'::regclass);


--
-- TOC entry 3348 (class 0 OID 41567)
-- Dependencies: 213
-- Data for Name: academic_plan; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3352 (class 0 OID 41590)
-- Dependencies: 217
-- Data for Name: department; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3350 (class 0 OID 41576)
-- Dependencies: 215
-- Data for Name: faculty; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3345 (class 0 OID 41538)
-- Dependencies: 210
-- Data for Name: group; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3344 (class 0 OID 41531)
-- Dependencies: 209
-- Data for Name: students; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3346 (class 0 OID 41550)
-- Dependencies: 211
-- Data for Name: teacher; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3361 (class 0 OID 0)
-- Dependencies: 212
-- Name: academic_plan_id_academic_plan_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.academic_plan_id_academic_plan_seq', 1, false);


--
-- TOC entry 3362 (class 0 OID 0)
-- Dependencies: 216
-- Name: department_id_department_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.department_id_department_seq', 1, false);


--
-- TOC entry 3363 (class 0 OID 0)
-- Dependencies: 214
-- Name: faculty_id_faculty_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.faculty_id_faculty_seq', 1, false);


--
-- TOC entry 3196 (class 2606 OID 41574)
-- Name: academic_plan academic_plan_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.academic_plan
    ADD CONSTRAINT academic_plan_pkey PRIMARY KEY (id_academic_plan);


--
-- TOC entry 3200 (class 2606 OID 41597)
-- Name: department department_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.department
    ADD CONSTRAINT department_pkey PRIMARY KEY (id_department);


--
-- TOC entry 3198 (class 2606 OID 41583)
-- Name: faculty faculty_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.faculty
    ADD CONSTRAINT faculty_pkey PRIMARY KEY (id_faculty);


--
-- TOC entry 3192 (class 2606 OID 41544)
-- Name: group group_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."group"
    ADD CONSTRAINT group_pkey PRIMARY KEY (id);


--
-- TOC entry 3190 (class 2606 OID 41537)
-- Name: students students_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.students
    ADD CONSTRAINT students_pkey PRIMARY KEY (id);


--
-- TOC entry 3194 (class 2606 OID 41556)
-- Name: teacher teacher_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.teacher
    ADD CONSTRAINT teacher_pkey PRIMARY KEY (id);


--
-- TOC entry 3202 (class 2606 OID 41603)
-- Name: academic_plan academic_plan_department_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.academic_plan
    ADD CONSTRAINT academic_plan_department_id_fkey FOREIGN KEY (department_id) REFERENCES public.department(id_department);


--
-- TOC entry 3204 (class 2606 OID 41598)
-- Name: department department_faculty_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.department
    ADD CONSTRAINT department_faculty_id_fkey FOREIGN KEY (faculty_id) REFERENCES public.faculty(id_faculty);


--
-- TOC entry 3203 (class 2606 OID 41584)
-- Name: faculty faculty_academic_plan_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.faculty
    ADD CONSTRAINT faculty_academic_plan_id_fkey FOREIGN KEY (academic_plan_id) REFERENCES public.academic_plan(id_academic_plan);


--
-- TOC entry 3201 (class 2606 OID 41608)
-- Name: students students_group_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.students
    ADD CONSTRAINT students_group_id_fkey FOREIGN KEY (group_id) REFERENCES public."group"(id) NOT VALID;


-- Completed on 2023-03-20 20:36:04

--
-- PostgreSQL database dump complete
--

