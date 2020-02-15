-- phpMyAdmin SQL Dump
-- version 4.9.2
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Feb 15, 2020 at 01:01 PM
-- Server version: 10.4.10-MariaDB
-- PHP Version: 7.1.33

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
-- Table structure for table `std_register`
--

CREATE TABLE `std_register` (
  `id` int(10) NOT NULL,
  `name` varchar(255) NOT NULL,
  `reg_no` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `class` varchar(255) NOT NULL,
  `sem` varchar(255) NOT NULL,
  `ph_no` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL,
  `address` varchar(755) NOT NULL,
  `gender` varchar(255) NOT NULL,
  `dob` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `std_register`
--

INSERT INTO `std_register` (`id`, `name`, `reg_no`, `password`, `class`, `sem`, `ph_no`, `email`, `address`, `gender`, `dob`) VALUES
(7, 'abhilash', '1002', 'F7BCD141', 'BCA', '2', '142141', 'asdas@gmail.com', 'asdkakhsd ashdajsd', 'Male', 'adas'),
(8, 'bharathi', '1003', '037310E3', 'BCA', '2', '9127389161', 'sadh@gmail.com', 'hasdgahs hasgdahskgd', 'Female', '12-12-1990'),
(9, 'kasdhajksh', '1004', '49428CB2', 'BCA', '2', '9120387123', 'ashdads@gmail.com', 'uasdksahdjash', 'Female', '12-12-1999'),
(10, 'jasdghha', '1005', 'B4F0A04E', 'BBM', '1', '989856556', 'ajdhkjsa@gmail.com', 'ajkdhajksdh', 'Female', '12-09-1989');

-- --------------------------------------------------------

--
-- Table structure for table `teacher_register`
--

CREATE TABLE `teacher_register` (
  `id` int(10) NOT NULL,
  `teach_id` varchar(50) NOT NULL,
  `password` varchar(255) NOT NULL,
  `teach_name` varchar(255) NOT NULL,
  `dept` varchar(255) NOT NULL,
  `desig` varchar(255) NOT NULL,
  `ph_no` varchar(50) NOT NULL,
  `email` varchar(255) NOT NULL,
  `address` varchar(755) NOT NULL,
  `gender` varchar(50) NOT NULL,
  `dob` varchar(75) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `teacher_register`
--

INSERT INTO `teacher_register` (`id`, `teach_id`, `password`, `teach_name`, `dept`, `desig`, `ph_no`, `email`, `address`, `gender`, `dob`) VALUES
(1, '112', 'A5847A40', 'abc', 'commerce', 'HOD', '998650651', 'asdas@gmail.com', 'akhdaksh kashdk jasjkdhajkhdjk ahdjkashdjkashjkdasjkdhajksdhajksdhkjahd asjkdh ajkhdjka skjdha kjhd', 'Male', '12-12-1990');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `login`
--
ALTER TABLE `login`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `std_register`
--
ALTER TABLE `std_register`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `teacher_register`
--
ALTER TABLE `teacher_register`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `login`
--
ALTER TABLE `login`
  MODIFY `id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `std_register`
--
ALTER TABLE `std_register`
  MODIFY `id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `teacher_register`
--
ALTER TABLE `teacher_register`
  MODIFY `id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
