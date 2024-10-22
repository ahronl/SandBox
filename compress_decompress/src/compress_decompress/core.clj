(ns compress-decompress.core)

;;;; compress a file and decompress it
(defn compress [file]
  (->> file
       slurp
       (#(clojure.string/split % #" "))
       (mapv clojure.string/lower-case)
       (reduce-kv (fn [m i w]
                     (if (get m w)
                        (update m w conj i)
                        (assoc m w [i]))) {})))



;(def c-pos (compress "/Users/aharon/t.txt"))

(defn decompress [xs]
  (->> xs
       (reduce-kv (fn [acc k v]
                    (loop [a acc vs v]
                      (if (empty? vs)
                        a
                        (recur (assoc a (first vs) k) (rest vs))))) {})
       (into (sorted-map))
       vals
       (clojure.string/join " ")))

;(def pos-str (decompress c-pos))
