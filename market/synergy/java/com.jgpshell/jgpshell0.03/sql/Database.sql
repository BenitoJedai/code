-- phpMyAdmin SQL Dump
-- version 2.6.1
-- http://www.phpmyadmin.net
-- 
-- Serveur: localhost
-- Généré le : Dimanche 23 Avril 2006 à 00:38
-- Version du serveur: 4.1.9
-- Version de PHP: 4.3.10
-- 
-- Base de données: `grille`
-- 

-- --------------------------------------------------------

-- 
-- Structure de la table `cards`
-- 

CREATE TABLE `cards` (
  `Name` varchar(128) NOT NULL default '',
  `Manufacturer` text NOT NULL,
  `Type` text NOT NULL,
  `Memory Size` int(11) NOT NULL default '0',
  `Memory Type` varchar(32) NOT NULL default '',
  `CPU frequency` int(11) NOT NULL default '0',
  `userKey` varchar(128) NOT NULL default '',
  `userKeyVersion` varchar(128) NOT NULL default '',
  `userKeyID` varchar(128) NOT NULL default '',
  `cardManagerID` varchar(128) NOT NULL default '',
  `maxBlockSize` int(11) NOT NULL default '0',
  PRIMARY KEY  (`Name`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- 
-- Contenu de la table `cards`
-- 

INSERT INTO `cards` VALUES ('Card1', 'Santa', 'BigBigOne', 1000, 'super_qualite', 220, 'userKey1', 'userKeyVersion1', 'userKeyID1', 'cardManagerID1', 0);
INSERT INTO `cards` VALUES ('Card2', 'JD', 'FastCard', 15, 'super_qualite', 2000, 'userKey2', 'userKeyVersion2', 'userKeyID2', 'cardManagerID2', 0);
INSERT INTO `cards` VALUES ('Card3', 'Cisco', 'White', 130, 'super_qualite', 260, 'userKey3', 'userKeyVersion3', 'userKeyID3', 'cardManagerID3', 0);
INSERT INTO `cards` VALUES ('Card4', 'IBM', 'BigBlue', 520, 'pas_tip_top', 96, 'userKey4', 'userKeyVersion4', 'userKeyID4', 'cardManagerID4', 0);
INSERT INTO `cards` VALUES ('Card5', 'MonsieurLeFabriquant', 'CarteSuperBien', 234, 'super_qualite', 512, 'userKey5', 'userKeyVersion5', 'userKeyID5', 'cardManagerID5', 0);
INSERT INTO `cards` VALUES ('card6', 'John', 'Jcard', 324, 'super_qualite', 432, 'userKey6', 'userKeyVersion6', 'userKeyID6', 'cardManagerID6', 0);
INSERT INTO `cards` VALUES ('OMNIKEY CardMan 3x21 0', 'ghtuyh', 'tuty', 0, '1', 0, '404142434445464748494a4b4c4d4e4f', '4564', 'fjhflhjfl', 'A000000003000000', 0);
        