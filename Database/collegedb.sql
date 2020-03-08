-- phpMyAdmin SQL Dump
-- version 5.0.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Mar 08, 2020 at 12:07 PM
-- Server version: 10.4.11-MariaDB
-- PHP Version: 7.2.27

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `collegedb`
--

-- --------------------------------------------------------

--
-- Table structure for table `assignments`
--

CREATE TABLE `assignments` (
  `id` int(10) NOT NULL,
  `dept` varchar(50) NOT NULL,
  `sem` varchar(10) NOT NULL,
  `name` varchar(100) NOT NULL,
  `_desc` varchar(1000) NOT NULL,
  `start_date` varchar(50) NOT NULL,
  `end_date` varchar(50) NOT NULL,
  `assigned_by` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `assignments`
--

INSERT INTO `assignments` (`id`, `dept`, `sem`, `name`, `_desc`, `start_date`, `end_date`, `assigned_by`) VALUES
(1, 'BCA', '1', 'asdasd a', 'adasdasdasd asd asda sdasda', '12-12-2009', '12-12-2009', 'asdasj das'),
(2, 'BCA', '6', 'adsadsad', 'adfdasfsafsadfsadf', '12-12-2020', '12-12-2021', 'asdasdasdasd');

-- --------------------------------------------------------

--
-- Table structure for table `events`
--

CREATE TABLE `events` (
  `id` int(10) NOT NULL,
  `dept` varchar(50) NOT NULL,
  `sem` varchar(10) NOT NULL,
  `event_name` varchar(255) NOT NULL,
  `_desc` varchar(1000) NOT NULL,
  `organised_by` varchar(255) NOT NULL,
  `from_date` varchar(50) NOT NULL,
  `to_date` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `events`
--

INSERT INTO `events` (`id`, `dept`, `sem`, `event_name`, `_desc`, `organised_by`, `from_date`, `to_date`) VALUES
(2, 'BCA', '1', '[sadfsadjfkh]', 'sadfasdf', 'sadfasdf', '15-03-2020', '27-03-2020'),
(3, 'BBM', '2', 'asdasd', 'asd', 'asdasd', '07-03-2020', '08-03-2020');

-- --------------------------------------------------------

--
-- Table structure for table `leaves`
--

CREATE TABLE `leaves` (
  `id` int(10) NOT NULL,
  `reg_no` varchar(255) NOT NULL,
  `dept` varchar(50) NOT NULL,
  `sem` varchar(10) NOT NULL,
  `leave_to` varchar(75) NOT NULL,
  `leave_from` varchar(75) NOT NULL,
  `leave_subject` varchar(250) NOT NULL,
  `reason` varchar(1000) NOT NULL,
  `from_date` varchar(50) NOT NULL,
  `to_date` varchar(50) NOT NULL,
  `leave_status` varchar(10) NOT NULL DEFAULT '0',
  `faculty_message` varchar(1000) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `leaves`
--

INSERT INTO `leaves` (`id`, `reg_no`, `dept`, `sem`, `leave_to`, `leave_from`, `leave_subject`, `reason`, `from_date`, `to_date`, `leave_status`, `faculty_message`) VALUES
(1, '1001', 'BCA', '4', 'asdkh', 'Bharathi', 'not well', 'fever', '2020-3-11', '2020-3-25', 'pending', ''),
(2, '1001', 'BCA', '1', 'asa', 'abhilash', 'fdgfddfg', 'dgfdg', '2020-03-08', '2020-3-9', 'pending', ''),
(3, '1001', 'BCA', '1', 'aab', 'abhilash', 'fdgfddfg', 'dgfdg', '2020-03-08', '2020-3-9', 'Reject', 'dsf');

-- --------------------------------------------------------

--
-- Table structure for table `login`
--

CREATE TABLE `login` (
  `id` int(10) NOT NULL,
  `username` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `login`
--

INSERT INTO `login` (`id`, `username`, `password`, `email`) VALUES
(1, 'admin', 'admin123', 'admin@gmail.com');

-- --------------------------------------------------------

--
-- Table structure for table `qpaper`
--

CREATE TABLE `qpaper` (
  `id` int(10) NOT NULL,
  `title` varchar(100) NOT NULL,
  `_desc` varchar(1000) NOT NULL,
  `dept` varchar(50) NOT NULL,
  `sem` varchar(10) NOT NULL,
  `url` varchar(1000) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `qpaper`
--

INSERT INTO `qpaper` (`id`, `title`, `_desc`, `dept`, `sem`, `url`) VALUES
(2, 'sajdh', 'asdasdasd', 'BCA', '4', 'http://localhost/phpmyadmin/tbl_structure.php?db=collegedb&table=qpaper');

-- --------------------------------------------------------

--
-- Table structure for table `std_register`
--

CREATE TABLE `std_register` (
  `id` int(10) NOT NULL,
  `name` varchar(255) NOT NULL,
  `reg_no` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `dept` varchar(255) NOT NULL,
  `sem` varchar(255) NOT NULL,
  `designation` varchar(100) NOT NULL,
  `ph_no` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL,
  `address` varchar(755) NOT NULL,
  `gender` varchar(255) NOT NULL,
  `dob` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `std_register`
--

INSERT INTO `std_register` (`id`, `name`, `reg_no`, `password`, `dept`, `sem`, `designation`, `ph_no`, `email`, `address`, `gender`, `dob`) VALUES
(1, 'abhilash', '1001', 'abc', 'BCA', '1', 'student', '097128936', 'asdgasd@gmail.com', 'iagdashkdgahgsd', 'Male', '06-03-2020'),
(2, 'abhilash', '1002', '4E2F4691', 'BCA', '1', 'student', '097128936', 'asdgasd@gmail.com', 'iagdashkdgahgsd', 'Male', '06-03-2020'),
(3, 'akjsdfhkajdfh', '1003', '02AC1FA2', 'BCA', '3', 'student', '9172368123', 'ajsdhg@gmail.com', 'hghaf', 'Male', '06-03-2020'),
(4, 'aab', '2002', 'abc', 'BCA', '1', 'faculty', '1293872091', 'ads2@gmail.com', 'ashdgasjkdh', 'Male', '29-02-2020'),
(5, 'asa', 'L13972', 'FBAB92AF', 'BCA', '1', 'faculty', '124087123', 'adfadf@gmail.com', 'dfasdfsadf', 'Male', '15-02-2020');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `assignments`
--
ALTER TABLE `assignments`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `events`
--
ALTER TABLE `events`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `leaves`
--
ALTER TABLE `leaves`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `login`
--
ALTER TABLE `login`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `qpaper`
--
ALTER TABLE `qpaper`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `std_register`
--
ALTER TABLE `std_register`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `assignments`
--
ALTER TABLE `assignments`
  MODIFY `id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `events`
--
ALTER TABLE `events`
  MODIFY `id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `leaves`
--
ALTER TABLE `leaves`
  MODIFY `id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `login`
--
ALTER TABLE `login`
  MODIFY `id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `qpaper`
--
ALTER TABLE `qpaper`
  MODIFY `id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `std_register`
--
ALTER TABLE `std_register`
  MODIFY `id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
