import Data.Char
import Control.Monad

main = do
    -- rs <- sequence [getLine,getLine,getLine]
    -- print rs 
    colors <- forM [1,2,3,4] (\x -> do
        putStrLn $ "Word for " ++ show x
        -- color <- getLine
        -- return color)
        getLine)
    putStrLn "Hey"
    mapM putStrLn $ zipWith (\y c -> show y ++ " " ++ c) [1,2,3,4] colors

-- main = do
--     return ()
--     return "HAHAHA"
--     line <- getLine
--     when (line == "secret") $ do
--         putStrLn "secret info"
--     return "BLAH BLAH BLAH"
--     num1 <- return 4
--     putStrLn $ show num1
--     print line

-- main = do
--     putStrLn "What is your name"
--     str <- getLine
--     let bigOne = map toUpper str
--     putStrLn $ "Hi " ++ bigOne

-- main = do
--     line <- getLine
--     if null line
--         then return ()
--         else do
--             putStrLn $ reverseLine line
--             putStrLn $ reverseWords line
--             main

reverseLine :: String -> String
-- reverseLine [] = []
-- reverseLine (x:xs) = (reverseLine xs) ++ [x] 
reverseLine = reverse

reverseWords :: String -> String
reverseWords = unwords . map reverse . words


-- main = getLine