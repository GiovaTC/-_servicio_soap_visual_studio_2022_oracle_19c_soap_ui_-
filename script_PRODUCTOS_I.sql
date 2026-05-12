-- =========================================
-- ELIMINAR OBJETOS SI EXISTEN
-- =========================================

BEGIN
    EXECUTE IMMEDIATE 'DROP TABLE PRODUCTOS_DETALLE CASCADE CONSTRAINTS';
EXCEPTION
    WHEN OTHERS THEN NULL;
END;
/

BEGIN
    EXECUTE IMMEDIATE 'DROP TABLE PRODUCTOS_I CASCADE CONSTRAINTS';
EXCEPTION
    WHEN OTHERS THEN NULL;
END;
/

BEGIN
    EXECUTE IMMEDIATE 'DROP SEQUENCE SEQ_PRODUCTOS';
EXCEPTION
    WHEN OTHERS THEN NULL;
END;
/

BEGIN
    EXECUTE IMMEDIATE 'DROP SEQUENCE SEQ_PRODUCTOS_DETALLE';
EXCEPTION
    WHEN OTHERS THEN NULL;
END;
/

COMMIT;

-- =========================================
-- TABLA PRINCIPAL
-- =========================================

CREATE TABLE PRODUCTOS_I
(
    ID_PRODUCTO NUMBER PRIMARY KEY,

    NOMBRE VARCHAR2(100),

    CATEGORIA VARCHAR2(100),

    FECHA_REGISTRO DATE
);

COMMIT;

-- =========================================
-- TABLA DETALLE
-- =========================================

CREATE TABLE PRODUCTOS_DETALLE
(
    ID_DETALLE NUMBER PRIMARY KEY,

    ID_PRODUCTO NUMBER,

    CODIGO VARCHAR2(50),

    NOMBRE VARCHAR2(100),

    DESCRIPCION VARCHAR2(200),

    PRECIO NUMBER(10,2),

    STOCK NUMBER,

    MARCA VARCHAR2(100),

    ESTADO VARCHAR2(50),

    CONSTRAINT FK_PRODUCTO
    FOREIGN KEY (ID_PRODUCTO)
    REFERENCES PRODUCTOS_I(ID_PRODUCTO)
);

COMMIT;

-- =========================================
-- SEQUENCE PRODUCTOS
-- =========================================

CREATE SEQUENCE SEQ_PRODUCTOS
START WITH 1
INCREMENT BY 1
NOCACHE
NOCYCLE;

COMMIT;

-- =========================================
-- SEQUENCE DETALLE
-- =========================================

CREATE SEQUENCE SEQ_PRODUCTOS_DETALLE
START WITH 1
INCREMENT BY 1
NOCACHE
NOCYCLE;

COMMIT;

-- =========================================
-- INSERT PRUEBA PRODUCTO
-- =========================================

INSERT INTO PRODUCTOS_I
(
    ID_PRODUCTO,
    NOMBRE,
    CATEGORIA,
    FECHA_REGISTRO
)
VALUES
(
    SEQ_PRODUCTOS.NEXTVAL,
    'Laptop Gamer',
    'Tecnologia',
    SYSDATE
);

COMMIT;

-- =========================================
-- INSERT PRUEBA DETALLE
-- =========================================

INSERT INTO PRODUCTOS_DETALLE
(
    ID_DETALLE,
    ID_PRODUCTO,
    CODIGO,
    NOMBRE,
    DESCRIPCION,
    PRECIO,
    STOCK,
    MARCA,
    ESTADO
)
VALUES
(
    SEQ_PRODUCTOS_DETALLE.NEXTVAL,
    1,
    'P001',
    'Mouse Gamer',
    'RGB Profesional',
    150000,
    5,
    'Logitech',
    'Activo'
);

COMMIT;

-- =========================================
-- CONSULTAS
-- =========================================

SELECT * FROM PRODUCTOS_I;

SELECT * FROM PRODUCTOS_DETALLE;