import Control.Monad
import Data.Char
import System.IO
import System.Directory
import System.Environment
import Data.List
import Control.Exception

-- main = forever $ do
--     l <- getLine
--     putStrLn $ map toUpper l

-- main = do
--     str <- getContents
--     putStrLn $ shortLinesOnly str

-- main = interact shortLinesOnly

-- shortLinesOnly :: String -> String
-- shortLinesOnly = unlines . filter (\l -> length l < 10) . lines 

-- main = do
--     handle <- openFile "haiku.txt" ReadMode
--     contents <- hGetContents handle
--     putStr contents
--     hClose handle

-- main = do
    -- withFile "haiku.txt" ReadMode (\handle -> do
    --     contents <- hGetContents handle
    --     putStr contents)
    -- INVALID:
    -- contents <- withFile "haiku.txt" ReadMode hGetContents 
    -- putStr contents
    -- t <- withFile "haiku.txt" ReadMode (\handle -> do
    --     contents <- hGetContents handle
    --     return contents)
    -- putStr t

-- main = do
--     contents <- readFile "haiku.txt"
--     writeFile "haikuUpper.txt" (map toUpper contents)

-- ADD TODO
-- main = do 
--     todoTask <- getLine
--     appendFile "todo.txt" $ todoTask ++ "\n" 

-- DELETE TODO
-- main = do
--     let fileName = "todo.txt"
--     contents <- readFile fileName
--     let todoTasks = lines contents
--         numberedTasks = zipWith (\n t -> show n ++ " " ++ t)
--                             [0..]
--                             todoTasks
--     putStrLn "here are your tasks"
--     mapM_ putStrLn  numberedTasks
--     numberString <- getLine
--     let number = read numberString
--         newToDoItems = unlines $ delete (todoTasks !! number) todoTasks
--     bracketOnError (openTempFile "." "temp")
--         (\(tempName, tempHandle) -> do
--             hClose tempHandle
--             removeFile tempName)
--         (\(tempName, tempHandle) -> do
--             hPutStr tempHandle newToDoItems
--             hClose tempHandle
--             removeFile fileName
--             renameFile tempName fileName)

--     (tempName, tempHandle) <- openTempFile "." "temp"
--     hPutStr tempHandle newToDoItems
--     hClose tempHandle
--     removeFile fileName
--     renameFile tempName fileName

-- main = do
--     args <- getArgs
--     progName <- getProgName
--     putStrLn $ "args: " ++ show args ++ "; name: " ++ progName

dispatch :: String -> [String] -> IO ()
dispatch "add" = add
dispatch "view" = view
dispatch "remove" = remove
dispatch command = doesntExist command
doesntExist :: String -> [String] -> IO ()

main = do
    (command:args) <- getArgs
    dispatch command args

add :: [String] -> IO ()
add [fileName, todoItem] = appendFile fileName (todoItem ++ "\n")
add _ = putStrLn "The add command takes exactly two arguments"

view :: [String] -> IO ()
view [fileName] = do
    contents <- readFile fileName
    let todoTasks = lines contents
        numberedTasks = zipWith (\n line -> show n ++ " - " ++ line)
            [0..] todoTasks
    putStr $ unlines numberedTasks

remove :: [String] -> IO ()
remove _ = do
    let fileName = "todo.txt"
    contents <- readFile fileName
    let todoTasks = lines contents
        numberedTasks = zipWith (\n t -> show n ++ " " ++ t)
                            [0..]
                            todoTasks
    putStrLn "here are your tasks"
    mapM_ putStrLn  numberedTasks
    numberString <- getLine
    let number = read numberString
        newToDoItems = unlines $ delete (todoTasks !! number) todoTasks
    bracketOnError (openTempFile "." "temp")
        (\(tempName, tempHandle) -> do
            hClose tempHandle
            removeFile tempName)
        (\(tempName, tempHandle) -> do
            hPutStr tempHandle newToDoItems
            hClose tempHandle
            removeFile fileName
            renameFile tempName fileName)

doesntExist command _ =
putStrLn $ "The " ++ command ++ " command doesn't exist"