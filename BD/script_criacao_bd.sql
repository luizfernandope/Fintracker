-- MySQL dump 10.13  Distrib 8.0.38, for Win64 (x86_64)
--
-- Host: localhost    Database: bd_fintracker
-- ------------------------------------------------------
-- Server version	8.0.39

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `admin`
--

DROP TABLE IF EXISTS `admin`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `admin` (
  `id_Admin` int NOT NULL AUTO_INCREMENT,
  `Nome` varchar(255) NOT NULL,
  `Email` varchar(255) NOT NULL,
  `Senha` varchar(255) NOT NULL,
  PRIMARY KEY (`id_Admin`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `caixa`
--

DROP TABLE IF EXISTS `caixa`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `caixa` (
  `id_Caixa` int NOT NULL AUTO_INCREMENT,
  `id_Pagamento` int NOT NULL,
  PRIMARY KEY (`id_Caixa`),
  KEY `id_Pagamento` (`id_Pagamento`),
  CONSTRAINT `caixa_ibfk_1` FOREIGN KEY (`id_Pagamento`) REFERENCES `pagamento` (`id_Pagamento`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `cliente`
--

DROP TABLE IF EXISTS `cliente`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `cliente` (
  `id_Cliente` int NOT NULL AUTO_INCREMENT,
  `Nome` varchar(255) NOT NULL,
  `Data_de_Cadastro` datetime NOT NULL,
  `CNPJ` varchar(14) NOT NULL,
  `Endereco` varchar(255) DEFAULT NULL,
  `Bairro` varchar(100) DEFAULT NULL,
  `Cidade` varchar(100) DEFAULT NULL,
  `Estado` varchar(2) DEFAULT NULL,
  `CEP` varchar(9) DEFAULT NULL,
  `Telefone` varchar(15) DEFAULT NULL,
  `Email` varchar(255) DEFAULT NULL,
  `Status` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id_Cliente`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `conta_a_pagar`
--

DROP TABLE IF EXISTS `conta_a_pagar`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `conta_a_pagar` (
  `id_Conta_a_Pagar` int NOT NULL AUTO_INCREMENT,
  `Valor` decimal(10,2) NOT NULL,
  `id_Fornecedor` int NOT NULL,
  `Metodo_de_Pagamento` varchar(50) DEFAULT NULL,
  `Data_de_Transacao` datetime NOT NULL,
  `Previsao_de_Termino` datetime DEFAULT NULL,
  `descricao` varchar(150) DEFAULT NULL,
  `id_Pagamento` int DEFAULT NULL,
  PRIMARY KEY (`id_Conta_a_Pagar`),
  KEY `id_Fornecedor` (`id_Fornecedor`),
  KEY `fk_fornecedor` (`id_Pagamento`),
  CONSTRAINT `conta_a_pagar_ibfk_1` FOREIGN KEY (`id_Fornecedor`) REFERENCES `fornecedor` (`id_Fornecedor`),
  CONSTRAINT `fk_fornecedor` FOREIGN KEY (`id_Pagamento`) REFERENCES `pagamento` (`id_Pagamento`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `conta_a_receber`
--

DROP TABLE IF EXISTS `conta_a_receber`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `conta_a_receber` (
  `id_Conta_a_Receber` int NOT NULL AUTO_INCREMENT,
  `Valor` decimal(10,2) NOT NULL,
  `id_Cliente` int NOT NULL,
  `Metodo_de_Pagamento` varchar(50) DEFAULT NULL,
  `Data_de_Transacao` datetime NOT NULL,
  `Previsao_de_Termino` datetime DEFAULT NULL,
  `descricao` varchar(150) DEFAULT NULL,
  `id_Pagamento` int DEFAULT NULL,
  PRIMARY KEY (`id_Conta_a_Receber`),
  KEY `id_Cliente` (`id_Cliente`),
  KEY `fk_fornecedor_conta_a_receber` (`id_Pagamento`),
  CONSTRAINT `conta_a_receber_ibfk_1` FOREIGN KEY (`id_Cliente`) REFERENCES `cliente` (`id_Cliente`),
  CONSTRAINT `fk_fornecedor_conta_a_receber` FOREIGN KEY (`id_Pagamento`) REFERENCES `pagamento` (`id_Pagamento`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `fornecedor`
--

DROP TABLE IF EXISTS `fornecedor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `fornecedor` (
  `id_Fornecedor` int NOT NULL AUTO_INCREMENT,
  `Nome` varchar(255) NOT NULL,
  `Data_de_Cadastro` datetime NOT NULL,
  `CNPJ` varchar(14) NOT NULL,
  `Endereco` varchar(255) DEFAULT NULL,
  `Bairro` varchar(100) DEFAULT NULL,
  `Cidade` varchar(100) DEFAULT NULL,
  `Estado` varchar(2) DEFAULT NULL,
  `CEP` varchar(9) DEFAULT NULL,
  `Telefone` varchar(15) DEFAULT NULL,
  `Email` varchar(255) DEFAULT NULL,
  `Status` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id_Fornecedor`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `itensvenda`
--

DROP TABLE IF EXISTS `itensvenda`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `itensvenda` (
  `id_Venda` int NOT NULL,
  `id_Produto` int NOT NULL,
  `quantidade` int NOT NULL,
  `preco` decimal(10,2) NOT NULL,
  PRIMARY KEY (`id_Venda`,`id_Produto`),
  KEY `id_Produto` (`id_Produto`),
  CONSTRAINT `itensvenda_ibfk_1` FOREIGN KEY (`id_Venda`) REFERENCES `venda` (`id_Venda`),
  CONSTRAINT `itensvenda_ibfk_2` FOREIGN KEY (`id_Produto`) REFERENCES `produto` (`id_Produto`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `pagamento`
--

DROP TABLE IF EXISTS `pagamento`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `pagamento` (
  `id_Pagamento` int NOT NULL AUTO_INCREMENT,
  `Data` datetime NOT NULL,
  `Metodo` varchar(50) DEFAULT NULL,
  `Tipo` varchar(50) DEFAULT NULL,
  `Parcelas` int DEFAULT NULL,
  `Valor` decimal(10,2) DEFAULT NULL,
  `descricao` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id_Pagamento`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `produto`
--

DROP TABLE IF EXISTS `produto`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `produto` (
  `id_Produto` int NOT NULL AUTO_INCREMENT,
  `Nome` varchar(255) NOT NULL,
  `Categoria` varchar(100) DEFAULT NULL,
  `Descricao` varchar(255) DEFAULT NULL,
  `Quantidade` int DEFAULT NULL,
  `Valor_Unitario` decimal(10,2) DEFAULT NULL,
  `Valor_Total` decimal(10,2) DEFAULT NULL,
  PRIMARY KEY (`id_Produto`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `venda`
--

DROP TABLE IF EXISTS `venda`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `venda` (
  `id_Venda` int NOT NULL AUTO_INCREMENT,
  `id_Cliente` int NOT NULL,
  `Status` varchar(50) DEFAULT NULL,
  `data_transacao` datetime DEFAULT NULL,
  `Metodo` varchar(50) DEFAULT NULL,
  `parcelas` int DEFAULT NULL,
  PRIMARY KEY (`id_Venda`),
  KEY `id_Cliente` (`id_Cliente`),
  CONSTRAINT `venda_ibfk_3` FOREIGN KEY (`id_Cliente`) REFERENCES `cliente` (`id_Cliente`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-06-15  9:00:34
