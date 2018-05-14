#[macro_use] extern crate text_io;

use std::ops::Fn;

static mut STATE0: u64 = 1;
static mut STATE1: u64 = 2;

unsafe fn random(max_value: i32) -> i32 {
    let mut s1: u64 = STATE0;
    let s0: u64 = STATE1;
    STATE0 = s0;
    s1 ^= s1 << 23;
    s1 ^= s1 >> 17;
    s1 ^= s0;
    s1 ^= s0 >> 26;
    STATE1 = s1;
    return ((STATE0 + STATE1) % max_value as u64) as i32;
}

fn read_int<F>(prompt: String, validate: F) -> i32 where F: Fn(i32) -> bool {
    println!("{}", prompt);
    let input: String = read!();
    match input.parse::<i32>() {
        Ok(i) if validate(i) => i,
        _ => read_int(prompt, validate)
    }
}

fn main() {
    let max = read_int("Enter a max greater than 0: ".to_string(), |x| x > 0);
    let magic = read_int("Enter a magic number in range: ".to_string(), |x| x >= 0 && x < max);    
    
}
