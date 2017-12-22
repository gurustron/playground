-- module Test where

-- sayHello = putStrLn "Hello: from module Test!"

-- sumSquares x y = x^2 + y^2

-- infixl 6 |-|
-- (|-|) x y = abs $ x - y

-- import Data.Char
-- twoDigits2Int :: Char -> Char -> Int
-- twoDigits2Int x y 
--     | (isNumber x) && (isNumber y) = 10 * (digitToInt x) + digitToInt y 
--     | otherwise = 100

-- dist :: (Double, Double) -> (Double, Double) -> Double
-- dist (x1,y1) (x2, y2) = sqrt $ (x1 -x2)^2 + (y1 - y2)^2

-- doubleFact :: Integer -> Integer
-- doubleFact n
--     | n <= 1 = 1
--     | otherwise =  n * (doubleFact ( n - 2))    

fibonacci :: Integer -> Integer
fibonacci 0         = 0
fibonacci 1         = 1
fibonacci n 
    | n > 1 = fibonacci (n-2) + fibonacci (n-1)
    | n < 0 = (-1)^(1-n) * fibonacci (-n)

-- fibonacci 0 = 0
-- fibonacci 1 = 1
-- fibonacci (-1) = (1)
-- fibonacci n
--     | n > 0  = fibonacci (n - 1) + fibonacci (n - 2)
--     | otherwise = fibonacci (n + 1) + fibonacci (n + 2)

fibonacci' :: Integer -> Integer
fibonacci' 0 = 0
fibonacci' 1 = 1
fibonacci' n = fibonacci (n - 1) + fibonacci (n - 2)

fibonacci'' :: Integer -> Integer
fibonacci'' n
    | n >= 0 = let
            fibhelper acc acc1 0 = acc
            fibhelper acc acc1 n = fibhelper acc1 (acc1+acc) (n-1)
        in fibhelper 0 1 n
    | otherwise = (-1)^(1-n) * fibonacci (-n) 



seqA :: Integer -> Integer
seqA n
    | n >= 0 = let
            seq' acc0 acc1 acc2 0 = acc0
            seq' acc0 acc1 acc2 n = seq' acc1 acc2 (acc2+acc1-2*acc0) (n-1) 
        in seq' 1 2 3 n
    | otherwise = error "must be 0 or more"

sum'n'count :: Integer -> (Integer, Integer)
sum'n'count x 
    | x == 0 = (0,1)
    | otherwise = let
        inner (s,c) 0 = (s,c)
        inner (s,c) n = inner (s + abs n `mod` 10, c+1)  $ abs n `div` 10
    in inner (0,0) x

        


-- sum'n'count :: Integer -> (Integer, Integer)
-- sum'n'count x = let
--         inner (s, c) n
--             | (s1, c1) 0 = (s1, c1)
--             | (s, c) n = inner (s, c+1) 1
--     in
--         inner (0,0) x