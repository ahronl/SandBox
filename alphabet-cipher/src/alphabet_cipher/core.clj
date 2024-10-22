(ns alphabet-cipher.core)

;;https://github.com/gigasquid/wonderland-clojure-katas/tree/master/alphabet-cipher

(def secret "scones")
(def message "meetmebythetree")
(def encripted-message "egsgqwtahuiljgs")

(defn mk-secret [s c]
  (->> s
       (repeat c)
       (clojure.string/join)
       (take c)
       (clojure.string/join)))

(defn letter-to-index [v i]
  (-> v (nth i) (int) (- 97)))

(defn mk-letters [offset]
  (mapv (fn [i] 
           (let [i (+ 97 (mod (+ i offset) 26))]
            (char i))) 
        (range 0 26)))

(defn encript [m s]
  (let [secret (mk-secret s (count m))]
        (->> (count m)
             (range 0)
             (map (fn [i]
                    (-> 
                        (letter-to-index m i)
                        (mk-letters)
                        (nth (letter-to-index secret i)))))
              (clojure.string/join))))

(defn decript [m s]
  (let [secret (mk-secret s (count m))
        letters-a-to-z (mk-letters 0)]
    (->> (count m)
         (range 0)
         (map (fn [i]
                (->
                    (letter-to-index secret i)
                    (mk-letters)
                    (.indexOf (nth m i))
                    (#(nth letters-a-to-z %)))))
          (clojure.string/join))))

(comment (= (encript message secret) encripted-message)
(= (decript encripted-message secret) message))
